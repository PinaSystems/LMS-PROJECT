using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Web;
using System.Security.Cryptography;

using Microsoft.Extensions.Logging;
using System.Linq;
using OAWA.Data.Models;
using OAWA.Data;
using OAWA.Data.Dtos;

namespace OAWA.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration config, IMapper mapper,
        UserManager<User> userManager, IUserRepository userRepository,
        SignInManager<User> signInManager, IEmailSender emailSender,
        ILogger<AuthController> logger)
        {
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailDto>(userToCreate);
            if (result.Succeeded)
            {
                return CreatedAtRoute("GetUser", new { controller = "Users", Id = userToCreate.Id }, userToReturn);
            }
            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [MapToApiVersion("1.0")] // this is nnot required as 1.0 is default
       // [MapToApiVersion("1.1")] // use this for a new api version implementation
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
            if (user == null)
                return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);
            if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                .Include(p => p.Role)
                .FirstOrDefaultAsync(u => u.NormalizedEmail == userForLoginDto.Email.ToUpper());
                // var roles= new List<RoleDto>();
                // appUser.UserRoles.ToList().ForEach(item => 
                // {
                //     roles.Add(new RoleDto (){ Id= item.Role.Id, Name = item.Role.NormalizedName });
                // });
                var userToReturn = _mapper.Map<UserForListDto>(appUser);
                // userToReturn.Roles= roles;
                var newRefreshToken = GenerateRefreshToken();
                await _userRepository.SaveRefreshToken(appUser.Id, newRefreshToken);
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    refreshToken = newRefreshToken,
                    user = userToReturn
                });
            }
            return Unauthorized();

        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.Name==null?"":user.Name)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((_config.GetSection("AppSettings:Token").Value))),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenRefreshDto tokenObj)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(tokenObj.Token);
                var username = principal.Identity.Name;
                var user = await _userManager.Users
                                    .FirstOrDefaultAsync(item => item.Email.Equals(principal.Identity.Name));
                if (user == null)
                {
                    throw new SecurityTokenException("Invalid  token");
                }
                var userId = user.Id;
                var savedRefreshTokens = await _userRepository.GetRefreshToken(username); //retrieve the refresh token from a data store
                if (!savedRefreshTokens.Contains(tokenObj.RefreshToken))
                    throw new SecurityTokenException("Invalid refresh token");
                var newJwtToken = GenerateJwtToken(user).Result;
                var newRefreshToken = GenerateRefreshToken();
                await _userRepository.DeleteRefreshToken(tokenObj);
                await _userRepository.SaveRefreshToken(userId, newRefreshToken);

                return new ObjectResult(new
                {
                    token = newJwtToken,
                    refreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving refresh token - " + ex.Message + "\n" + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(TokenRefreshDto refreshToken)
        {
            return Ok(await _userRepository.DeleteRefreshToken(refreshToken));
        }
    }

}
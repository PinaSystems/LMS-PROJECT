using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OAWA.Data.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Jupiter.API.Helpers;
using OAWA.Data.Dtos;
using OAWA.Data;
using OAWA.Data.Models;

namespace OAWA.API.Controllers
{
    //todo: change back
    //[ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IImageWriter _writer;
        public UsersController(DataContext context, IUserRepository repo, IMapper mapper, UserManager<User> userManager, IEmailSender emailSender, ILogger<UsersController> logger, IImageWriter writer)
        {
            _context = context;
            _mapper = mapper;
            _repo = repo;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _writer = writer;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            if (userParams.UserId != 0)
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userFromRepo = await _repo.GetUser(currentUserId);
                userParams.UserId = currentUserId;
            }

            var users = await _repo.GetUsers(userParams);
            var userListDto = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(userListDto);

        }

        [AllowAnonymous]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _repo.GetRoles());
        }
        
        [Authorize]
        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRolesByUser(int Id)
        {
            var user = await _repo.GetRolesByUserId(Id);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetUser"),]
        public async Task<IActionResult> GetUser(int Id)
        {
            try
            {
                var user = await _repo.GetUser(Id);
                var userView = new UserForViewDto();
                _mapper.Map(user, userView);
                var currentUser= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                
                return Ok(userView);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting a user-" + ex.InnerException.Message + "\n" + ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        
    }
}
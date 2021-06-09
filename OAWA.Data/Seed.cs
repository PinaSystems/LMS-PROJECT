using System.Collections.Generic;
using System.Linq;
using OAWA.Data.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace OAWA.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var roles = new List<Role>
                {
                    new Role{Name = "SuperAdmin"},
                    new Role{Name = "Administrator"},
                    new Role{Name = "Trainer"},                                        
                    new Role{Name = "Learner"},                                        
                    new Role{Name = "Editor"},
                    new Role{Name = "Alumni"}, 
                    new Role{Name = "Sales"},
                    new Role{Name = "Support"},
                    new Role{Name = "Lead"},                                       
                };
                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }
                // foreach (var user in users)
                // {
                //     _userManager.CreateAsync(user, "password").Wait();
                //     _userManager.AddToRoleAsync(user, "EndUser").Wait();
                // }

                var adminUser = new User()
                {
                    UserName = "admin@oawa.com",
                    Email="admin@oawa.com"
                };
                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("admin@oawa.com").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Administrator", "Trainer", "Learner","Editor","Alumni", "Sales","Support", "Lead" }).Wait();
                }
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA1())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }
    }
}
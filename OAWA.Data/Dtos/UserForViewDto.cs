using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using OAWA.Data.Models;

namespace OAWA.Data.Dtos
{
    public class UserForViewDto: IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string ContactEmail { get; set; }
    }
}
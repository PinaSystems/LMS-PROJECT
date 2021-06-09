using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using OAWA.Data.Enums;

namespace OAWA.Data.Models
{
    public class User : IdentityUser<long>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public string ProfilePhotoUrl { get; set; }

        public bool IsDeleted { get; set; }
        public string ContactEmail { get; set; }
        public LoginStatusType LoginStatus { get; set; }
        public UserStatusType UserStatus { get; set; }
        public PaymentStatusType PaymentStatus { get; set; }
        public float EntranceTestScore { get; set; }
        public EntranceTestType EntranceTestStatus { get; set; }
        public UserCategoryType UserCategory { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OAWA.Data.Models
{
    public class Role: IdentityRole<long>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
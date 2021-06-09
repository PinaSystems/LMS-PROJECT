using System;
using System.Collections.Generic;
using OAWA.Data.Enums;

namespace OAWA.Data.Dtos
{
    public class UserForListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }        
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }        
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public bool IsEmailConfirmed { get; set; }

    }
}
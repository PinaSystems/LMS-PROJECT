using System;

namespace OAWA.Data.Dtos
{
    public class UserForUpdateDto
    {        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Otp { get; set; }
        public string PasswordHash { get; set; }
        public long CompanyId { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string FavouriteQuote { get; set; }
        public string Goal { get; set; }
        public string Designation { get; set; }
        public string JoiningYear { get; set; }
        public string AboutMe { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Gender { get; set; }
        public long RoleId { get; set; }
        public string Base64ImageFile { get; set; }
        public string ContactEmail { get; set; }
        public string MyThingsPic { get; set; }

    }
}
using System;

namespace OAWA.Data.Models
{
    public class LoginHistory
    {
        public long Id { get; set; }
        public long UserId {get; set;}
        public User User { get; set; }
        public DateTime LoginDate{get; set;}
        public DateTime LogoutDate {get; set;}
        public int SessionTime {get; set;}
    }
}
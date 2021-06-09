using OAWA.Data.Models;

namespace OAWA.Data
{
    public class UserSession
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string RefreshToken { get; set; }
        public string Device { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
using OAWA.Data.Enums;

namespace OAWA.Data.Models
{
    public class LearnerChoice
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public LikeDislikeType LikeDislike { get; set; }
        public string ContentComment { get; set; }
        public bool IsBookMarked { get; set; }
    }
}
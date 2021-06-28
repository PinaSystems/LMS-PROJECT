using OAWA.Data.Enums;

namespace OAWA.Data.Models
{
    public class NuggetLikeDislike
    {
        public long Id { get; set; }
        public long NuggetId { get; set; }
        public Nugget Nugget { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public LikeDislikeType LikeDislikeType { get; set; }
    }
}
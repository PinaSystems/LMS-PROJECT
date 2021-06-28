using System;

namespace OAWA.Data.Models
{
    public class NuggetComments
    {
        public long Id { get; set; }
        public long NuggetId { get; set; }
        public Nugget Nugget { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime CreateddDate { get; set; }
    }
}
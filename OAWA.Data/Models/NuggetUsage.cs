using System;

namespace OAWA.Data.Models
{
    public class NuggetUsage
    {
        public long Id { get; set; }
        public long NuggetId { get; set; }
        public Nugget Nugget { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public int ElapsedTime { get; set; }
        public int SlideNumber { get; set; }
    }
}
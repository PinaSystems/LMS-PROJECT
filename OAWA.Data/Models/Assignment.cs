using System;

namespace OAWA.Data.Models
{
    public class Assignment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string AttachmentFile { get; set; }
        public long NuggetId { get; set; }
        public long LessonId { get; set; }
        public int MaxScore { get; set; }
    }
}
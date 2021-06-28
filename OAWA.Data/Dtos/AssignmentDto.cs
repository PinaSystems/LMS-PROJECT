using System;

namespace OAWA.Data.Dtos
{
    public class AssignmentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string AttachmentFile { get; set; }
        public long NuggetId { get; set; }
        public long LessonId { get; set; }
    }
}
using System;
using OAWA.Data.Enums;

namespace OAWA.Data.Models
{
    public class Nugget
    {
        public long Id { get; set; }
        public long BatchId { get; set; }
        public long LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SequenceNo { get; set; }
        public DateTime ClassDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public NuggetStatus NuggetStatus { get; set; }
    }
}
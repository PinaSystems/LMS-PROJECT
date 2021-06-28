using System;

namespace OAWA.Data.Models
{
    public class Lesson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
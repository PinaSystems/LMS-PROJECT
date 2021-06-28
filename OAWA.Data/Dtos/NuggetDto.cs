using System;

namespace OAWA.Data.Dtos
{
    public class NuggetDto
    {
        public long NuggetId { get; set; }
        public string Lesson { get; set; }
        public long LessonId { get; set; }
        public string Nugget { get; set; }
        public string ClassDate { get; set; }
    }
}
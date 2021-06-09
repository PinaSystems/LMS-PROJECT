namespace OAWA.Data.Models
{
    public class Course
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? ContentId { get; set; }
        public Content Content { get; set; }
    }
}
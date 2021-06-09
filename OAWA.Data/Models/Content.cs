namespace OAWA.Data.Models
{
    public class Content
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public long? DependencyId { get; set; }
        public Content Dependency { get; set; }
        public string CourseEligibility { get; set; }
    }
}
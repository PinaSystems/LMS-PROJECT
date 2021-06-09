using System;

namespace OAWA.Data.Dtos
{
    public class NewsletterForViewDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FileType { get; set; }
    }
}
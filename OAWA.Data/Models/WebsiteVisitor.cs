using System;

namespace OAWA.Data.Models
{
    public class WebsiteVisitor
    {
        public long Id { get; set; }
        public string Ip { get; set; }
        public string PagesSurfed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
namespace OAWA.Data.Helpers
{
    public class ListParams
    {
        private const int MaxPageSize =40;
        public int Page { get; set; } = 1;
        private int per_page = 10;
        public int Per_Page
        {
            get { return per_page; }
            set { per_page = value > MaxPageSize ? MaxPageSize : value; }
        }
        public string OrderBy { get; set; }
        public string Order { get; set; }
        public string Keyword { get; set; }
    }
}
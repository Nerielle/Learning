namespace Sandbox.ViewModels
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            SortField = "Id";
            SortOrder = SortOrder.Asc;
            CurrentPage = 1;
            PageSize = 3;
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public SortOrder SortOrder { get; set; }
        public string Sort
        {
            get { return string.Format("{0} {1}", SortField, SortOrder); }
        }
    }
}
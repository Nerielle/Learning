using Newtonsoft.Json;
using Sandbox.ViewModels;

namespace Sandbox.ViewModels
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            SortField = "Id";
            SortOrder = SortOrder.ASC;
            CurrentPage = 1;
            PageSize = 3;
        }
        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "sortField")]
        public string SortField { get; set; }

        [JsonProperty(PropertyName = "sortOrder")]
        public SortOrder SortOrder { get; set; }

        [JsonIgnore]
        public string Sort
        {
            get { return string.Format("{0} {1}", SortField, SortOrder); }
        }
    }
}
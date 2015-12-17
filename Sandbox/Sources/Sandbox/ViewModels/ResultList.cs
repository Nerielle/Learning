using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sandbox.ViewModels
{
    public class ResultList<T>
    {
        public ResultList(List<T> results, QueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
            Results = results;
        }

        [JsonProperty(PropertyName = "queryOptions")]
        public QueryOptions QueryOptions { get; private set; }

        [JsonProperty(PropertyName = "results")]
        public List<T> Results { get; private set; }
    }
}
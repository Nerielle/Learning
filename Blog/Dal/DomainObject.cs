using System;
using Newtonsoft.Json;

namespace Dal.Mapping
{
    public class DomainObject
    {
        [JsonProperty(PropertyName = "id")]
        public virtual Guid Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public virtual DateTime Date { get; set; }
    }
}
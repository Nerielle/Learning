using System;
using Dal.Mapping;
using Newtonsoft.Json;

namespace Dal
{
    public class Comment : DomainObject
    {
        [JsonProperty(PropertyName = "content")]
        public virtual string Content { get; set; }

        [JsonProperty(PropertyName = "articleId")]
        public virtual Guid ArticleId { get; set; }
    }
}
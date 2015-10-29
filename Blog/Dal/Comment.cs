using System;
using Dal.Mapping;
using Newtonsoft.Json;

namespace Dal
{
    public class Comment:DomainObject
    {
        [JsonProperty("content")]
        public virtual string Content { get; set; }
        [JsonProperty("articleid")]
        public virtual int ArticleId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Dal.Mapping;
using Newtonsoft.Json;

namespace Dal
{
    public class Article : DomainObject
    {
        public Article()
        {
            Comments = new List<Comment>();
        }

        [JsonProperty(PropertyName = "title")]
        public virtual string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public virtual string Description { get; set; }

        [JsonProperty(PropertyName = "content")]
        public virtual string Content { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public virtual IList<Comment> Comments { get; set; }
    }
}

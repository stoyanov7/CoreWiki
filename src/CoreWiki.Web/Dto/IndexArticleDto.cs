namespace CoreWiki.Web.Dto
{
    using System.Collections.Generic;
    using Models;
    using NodaTime;
    using Utilities.Infrastructure.Contracts;

    public class IndexArticleDto : IMapFrom<Article>
    {
        public string Topic { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public Instant Published { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
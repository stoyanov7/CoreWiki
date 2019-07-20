namespace CoreWiki.Application.Dto
{
    using System.Collections.Generic;
    using Models;
    using NodaTime;
    using Utilities.Infrastructure.Contracts;

    public class LatestArticleDto : IMapFrom<Article>
    {
        public string Topic { get; set; }

        public string Slug { get; set; }

        public Instant Published { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
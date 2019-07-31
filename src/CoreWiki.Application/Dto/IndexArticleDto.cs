namespace CoreWiki.Application.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Mapping;
    using Models;
    using NodaTime;

    public class IndexArticleDto : IMapFrom<Article>
    {
        public string Topic { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public Instant Published { get; set; }

        [Display(Name = "Views")]
        public long ViewCount { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
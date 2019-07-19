namespace CoreWiki.Application.Dto
{
    using Models;
    using NodaTime;
    using Utilities.Infrastructure.Contracts;

    public class AllArticlesDto : IMapFrom<Article>
    {
        public string Topic { get; set; }

        public string Slug { get; set; }

        public Instant Published { get; set; }
    }
}
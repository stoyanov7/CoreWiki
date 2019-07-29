namespace CoreWiki.Application.Dto
{
    using Domain.Mapping;
    using Models;
    using NodaTime;

    public class AllArticlesDto : IMapFrom<Article>
    {
        public string Topic { get; set; }

        public string Slug { get; set; }

        public Instant Published { get; set; }
    }
}
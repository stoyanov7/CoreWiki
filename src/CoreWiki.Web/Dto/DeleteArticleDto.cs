namespace CoreWiki.Web.Dto
{
    using System;
    using Domain.Mapping;
    using Models;

    public class DeleteArticleDto : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }
    }
}
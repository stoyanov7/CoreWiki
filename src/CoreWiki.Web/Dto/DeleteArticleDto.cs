namespace CoreWiki.Web.Dto
{
    using System;
    using Models;
    using Utilities.Infrastructure.Contracts;

    public class DeleteArticleDto : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }
    }
}
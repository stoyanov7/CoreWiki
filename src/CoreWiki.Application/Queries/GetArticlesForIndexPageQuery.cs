namespace CoreWiki.Application.Queries
{
    using System.Collections.Generic;
    using Dto;
    using MediatR;

    public class GetArticlesForIndexPageQuery : IRequest<IList<IndexArticleDto>>
    {
        public GetArticlesForIndexPageQuery(IList<IndexArticleDto> article)
        {
            this.Article = article;
        }

        public IList<IndexArticleDto> Article { get; set; }
    }
}
namespace CoreWiki.Application.Queries
{
    using System.Collections.Generic;
    using MediatR;

    public class GetLatestArticlesQuery : IRequest<IEnumerable<LatestArticleDto>>
    {
        public GetLatestArticlesQuery(IEnumerable<LatestArticleDto> article)
        {
            this.Article = article;
        }

        public IEnumerable<LatestArticleDto> Article { get; set; }
    }
}
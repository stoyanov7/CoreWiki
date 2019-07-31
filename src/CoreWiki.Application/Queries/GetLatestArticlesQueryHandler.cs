namespace CoreWiki.Application.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using Dto;
    using MediatR;

    public class GetLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, IEnumerable<LatestArticleDto>>
    {
        private readonly IArticleService articleService;

        public GetLatestArticlesQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<IEnumerable<LatestArticleDto>> Handle(
            GetLatestArticlesQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService.GetLatestArticle<LatestArticleDto>();
        }
    }
}
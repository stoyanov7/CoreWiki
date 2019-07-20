namespace CoreWiki.Application.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dto;
    using MediatR;
    using Services.Contracts;

    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<AllArticlesDto>>
    {
        private readonly IArticleService articleService;

        public GetAllArticlesQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<IEnumerable<AllArticlesDto>> Handle(
            GetAllArticlesQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService
                .GetAllArticlesAsync<AllArticlesDto>(request.PageNumber, request.PageSize);
        }
    }
}
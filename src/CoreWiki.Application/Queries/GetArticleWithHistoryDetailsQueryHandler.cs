namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Models;
    using Services.Contracts;

    public class GetArticleWithHistoryDetailsQueryHandler : IRequestHandler<GetArticleWithHistoryDetailsQuery, Article>
    {
        private readonly IArticleService articleService;

        public GetArticleWithHistoryDetailsQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Article> Handle(
            GetArticleWithHistoryDetailsQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService
                .GetArticleWithHistoryDetails<Article>(request.Slug);
        }
    }
}
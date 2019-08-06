namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using MediatR;
    using Models;

    public class GetHistoryQueryHandler : IRequestHandler<GetArticleHistoryQuery, Article[]>
    {
        private readonly IArticleService articleService;

        public GetHistoryQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Article[]> Handle(
            GetArticleHistoryQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService
                .GetHistory<Article>(request.Compare);
        }
    }
}
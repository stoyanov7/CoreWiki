namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Services.Contracts;

    public class GetArticlesCountQueryHandler : IRequestHandler<GetArticlesCountQuery, int>
    {
        private readonly IArticleService articleService;

        public GetArticlesCountQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public Task<int> Handle(
            GetArticlesCountQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.articleService.GetCount());
        }
    }
}
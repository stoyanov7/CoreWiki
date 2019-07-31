namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using MediatR;

    public class IsArticleExistQueryHandler : IRequestHandler<IsArticleExistQuery, bool>
    {
        private readonly IArticleService articleService;

        public IsArticleExistQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public Task<bool> Handle(
            IsArticleExistQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.articleService.IsArticleExist(request.Topic));
        }
    }
}
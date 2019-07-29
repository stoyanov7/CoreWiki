namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using MediatR;
    using Models;

    public class GetArticleHistoryAndAuthorQueryHandler : IRequestHandler<GetArticleHistoryAndAuthorQuery, Article>
    {
        private readonly IArticleService articleService;

        public GetArticleHistoryAndAuthorQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Article> Handle(
            GetArticleHistoryAndAuthorQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService
                .GetArticleHistoryAndAuthor<Article>(request.Slug);
        }
    }
}
namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Models;
    using Services.Contracts;

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
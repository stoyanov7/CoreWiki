namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using MediatR;
    using Models;

    public class GetArticleForEditQueryHandler : IRequestHandler<GetArticleForEditQuery, Article>
    {
        private readonly IArticleService articleService;

        public GetArticleForEditQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Article> Handle(
            GetArticleForEditQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService
                .FindBySlugAsync<Article>(request.Slug);
        }
    }
}
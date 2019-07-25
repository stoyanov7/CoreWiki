namespace CoreWiki.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Models;
    using Services.Contracts;

    public class GetArticleDetailsQueryHandler : IRequestHandler<GetArticleDetailsQuery, Article>
    {
        private readonly IArticleService articleService;

        public GetArticleDetailsQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<Article> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
        {
            return await this.articleService.DetailsAsync<Article>(request.Slug);
        }
    }
}
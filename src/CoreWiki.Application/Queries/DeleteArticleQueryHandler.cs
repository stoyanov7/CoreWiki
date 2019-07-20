namespace CoreWiki.Application.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Models;
    using Services.Contracts;
    using Utilities.Infrastructure.Contracts;

    public class DeleteArticleQueryHandler
        : IRequestHandler<DeleteArticleQuery, DeleteArticleDto>
    {
        private readonly IArticleService articleService;

        public DeleteArticleQueryHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<DeleteArticleDto> Handle(
            DeleteArticleQuery request,
            CancellationToken cancellationToken)
        {
            return await this.articleService.FindBySlugAsync<DeleteArticleDto>(request.Slug);
        }
    }

    public class DeleteArticleDto : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }
    }
}
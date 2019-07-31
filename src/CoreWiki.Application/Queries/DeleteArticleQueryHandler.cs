namespace CoreWiki.Application.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Mapping;
    using Domain.Services;
    using MediatR;
    using Models;

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
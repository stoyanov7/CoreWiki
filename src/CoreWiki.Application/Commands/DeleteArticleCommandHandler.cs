namespace CoreWiki.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Services.Contracts;

    public class DeleteArticleCommandHandler : AsyncRequestHandler<DeleteArticleCommand>
    {
        private readonly IArticleService articleService;

        public DeleteArticleCommandHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        protected override async Task Handle(
            DeleteArticleCommand request,
            CancellationToken cancellationToken)
        {
            await this.articleService.Delete(request.Slug);
        }
    }
}
namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Services.Contracts;

    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, CommandResult>
    {
        private readonly IArticleService articleService;

        public DeleteArticleCommandHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<CommandResult> Handle(
            DeleteArticleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.articleService.Delete(request.Slug);

                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                return CommandResult.Error(new DeleteArticleException("There was an error deleting the article", ex));
            }
        }
    }
}
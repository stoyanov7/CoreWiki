namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using MediatR;

    public class CanAuthorBeNotifiedCommandHandler : IRequestHandler<CanAuthorBeNotifiedCommand, CommandResult>
    {
        private readonly IArticleService articleService;

        public CanAuthorBeNotifiedCommandHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public async Task<CommandResult> Handle(
            CanAuthorBeNotifiedCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.articleService.CanAuthorBeNotified(request.AuthorId, request.EncodedUrl);

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}
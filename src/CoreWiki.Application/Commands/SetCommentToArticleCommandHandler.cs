namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class SetCommentToArticleCommandHandler : IRequestHandler<SetCommentToArticleCommand, CommandResult>
    {
        private readonly ICommentService commentService;
        private readonly ILogger<SetCommentToArticleCommandHandler> logger;

        public SetCommentToArticleCommandHandler(
            ICommentService commentService,
            ILogger<SetCommentToArticleCommandHandler> logger)
        {
            this.commentService = commentService;
            this.logger = logger;
        }

        public async Task<CommandResult> Handle(
            SetCommentToArticleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.commentService.SetCommentToArticleAsync(request.Comment, request.Article);

                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
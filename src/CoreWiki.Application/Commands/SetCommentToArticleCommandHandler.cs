namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Logger;
    using Domain.Services;
    using MediatR;

    public class SetCommentToArticleCommandHandler : IRequestHandler<SetCommentToArticleCommand, CommandResult>
    {
        private readonly ICommentService commentService;
        private readonly IMyLogger<SetCommentToArticleCommandHandler> logger;

        public SetCommentToArticleCommandHandler(
            ICommentService commentService,
            IMyLogger<SetCommentToArticleCommandHandler> logger)
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
                this.logger.Error(ex.Message);
                throw;
            }
        }
    }
}
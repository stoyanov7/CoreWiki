namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Logger;
    using Domain.Services;
    using Exceptions;
    using MediatR;

    public class CreateNewArticleCommandHandler : IRequestHandler<CreateNewArticleCommand, CommandResult>
    {
        private readonly IArticleService articleService;
        private readonly IMyLogger<CreateNewArticleCommand> logger;

        public CreateNewArticleCommandHandler(
            IArticleService articleService,
            IMyLogger<CreateNewArticleCommand> logger)
        {
            this.articleService = articleService;
            this.logger = logger;
        }

        public async Task<CommandResult> Handle(
            CreateNewArticleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.articleService
                    .Create(request.Topic, request.Content, request.AuthorId);

                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);

                return CommandResult.Error(new CreateNewArticleException("There was an error creating the article", ex));
            }
        }
    }
}
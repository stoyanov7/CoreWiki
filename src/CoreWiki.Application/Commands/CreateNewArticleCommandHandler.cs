namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Services;
    using Exceptions;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CreateNewArticleCommandHandler : IRequestHandler<CreateNewArticleCommand, CommandResult>
    {
        private readonly IArticleService articleService;
        private readonly ILogger<CreateNewArticleCommand> logger;

        public CreateNewArticleCommandHandler(
            IArticleService articleService,
            ILogger<CreateNewArticleCommand> logger)
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
                this.logger.LogError(ex.Message);

                return CommandResult.Error(new CreateNewArticleException("There was an error creating the article", ex));
            }
        }
    }
}
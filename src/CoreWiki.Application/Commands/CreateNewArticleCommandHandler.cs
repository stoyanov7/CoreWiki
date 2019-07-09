namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;

    public class CreateNewArticleCommandHandler : AsyncRequestHandler<CreateNewArticleCommand>
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

        protected override async Task Handle(
            CreateNewArticleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.articleService.Create(request.Topic, request.Content, request.AuthorId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);

                throw new CreateNewArticleException();
            }
        }
    }
}
namespace CoreWiki.Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Repository.Contracts;
    using Utilities;

    public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, CommandResult>
    {
        private readonly IArticleRepository articleRepository;

        public EditArticleCommandHandler(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<CommandResult> Handle(
            EditArticleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.articleRepository.UpdateAsync(request.Article);

                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                return CommandResult.Error(ex);
            }
        }
    }
}
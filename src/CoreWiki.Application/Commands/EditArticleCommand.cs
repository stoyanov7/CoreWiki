namespace CoreWiki.Application.Commands
{
    using MediatR;
    using Models;

    public class EditArticleCommand : IRequest<CommandResult>
    {
        public EditArticleCommand(Article article)
        {
            this.Article = article;
        }

        public Article Article { get; set; }
    }
}
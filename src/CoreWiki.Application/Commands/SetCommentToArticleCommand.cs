namespace CoreWiki.Application.Commands
{
    using MediatR;
    using Models;

    public class SetCommentToArticleCommand : IRequest<CommandResult>
    {
        public SetCommentToArticleCommand(Comment comment, Article article)
        {
            this.Comment = comment;
            this.Article = article;
        }

        public Comment Comment { get; set; }

        public Article Article { get; set; }
    }
}
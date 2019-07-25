namespace CoreWiki.Application.Commands
{
    using MediatR;

    public class DeleteArticleCommand : IRequest<CommandResult>
    {
        public DeleteArticleCommand(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
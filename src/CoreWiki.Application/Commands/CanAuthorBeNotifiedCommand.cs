namespace CoreWiki.Application.Commands
{
    using MediatR;

    public class CanAuthorBeNotifiedCommand : IRequest<CommandResult>
    {
        public CanAuthorBeNotifiedCommand(string authorId, string encodedUrl)
        {
            this.AuthorId = authorId;
            this.EncodedUrl = encodedUrl;
        }

        public string AuthorId { get; set; }

        public string EncodedUrl { get; set; }
    }
}
namespace CoreWiki.Application.Commands
{
    using MediatR;

    public class CreateNewArticleCommand : IRequest
    {
        public CreateNewArticleCommand(string topic, string content, string authorId)
        {
            this.Topic = topic;
            this.Content = content;
            this.AuthorId = authorId;
        }

        public string Topic { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }
    }
}

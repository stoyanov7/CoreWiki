namespace CoreWiki.Application.Commands
{
    using MediatR;

    public class IncrementArticleViewCountCommand : IRequest
    {
        public IncrementArticleViewCountCommand(string topic)
        {
            this.Topic = topic;
        }

        public string Topic { get; set; }
    }
}
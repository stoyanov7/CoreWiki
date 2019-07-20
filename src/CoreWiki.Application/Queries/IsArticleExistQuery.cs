namespace CoreWiki.Application.Queries
{
    using MediatR;

    public class IsArticleExistQuery : IRequest<bool>
    {
        public IsArticleExistQuery(string topic)
        {
            this.Topic = topic;
        }

        public string Topic { get; set; }
    }
}
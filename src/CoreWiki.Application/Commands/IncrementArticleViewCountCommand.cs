namespace CoreWiki.Application.Commands
{
    using MediatR;

    public class IncrementArticleViewCountCommand : IRequest
    {
        public IncrementArticleViewCountCommand(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
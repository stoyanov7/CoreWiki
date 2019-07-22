namespace CoreWiki.Application.Queries
{
    using MediatR;
    using Models;

    public class GetArticleWithHistoryDetailsQuery : IRequest<Article>
    {
        public GetArticleWithHistoryDetailsQuery(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
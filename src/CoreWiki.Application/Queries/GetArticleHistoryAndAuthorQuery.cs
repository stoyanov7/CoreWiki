namespace CoreWiki.Application.Queries
{
    using MediatR;
    using Models;

    public class GetArticleHistoryAndAuthorQuery : IRequest<Article>
    {
        public GetArticleHistoryAndAuthorQuery(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
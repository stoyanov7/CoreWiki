namespace CoreWiki.Application.Queries
{
    using MediatR;
    using Models;

    public class GetArticleForEditQuery : IRequest<Article>
    {
        public GetArticleForEditQuery(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
namespace CoreWiki.Application.Queries
{
    using MediatR;
    using Models;

    public class GetArticleDetailsQuery : IRequest<Article>
    {
        public GetArticleDetailsQuery(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
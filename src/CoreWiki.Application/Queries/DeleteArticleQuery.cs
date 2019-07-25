namespace CoreWiki.Application.Queries
{
    using MediatR;

    public class DeleteArticleQuery : IRequest<DeleteArticleDto>
    {
        public DeleteArticleQuery(string slug)
        {
            this.Slug = slug;
        }

        public string Slug { get; set; }
    }
}
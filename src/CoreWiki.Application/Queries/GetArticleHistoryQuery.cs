namespace CoreWiki.Application.Queries
{
    using MediatR;
    using Models;

    public class GetArticleHistoryQuery : IRequest<ArticleHistory[]>
    {
        public GetArticleHistoryQuery(string[] compare)
        {
            this.Compare = compare;
        }

        public string[] Compare { get; set; }
    }
}
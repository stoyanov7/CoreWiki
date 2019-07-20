namespace CoreWiki.Services.Contracts
{
    using System.Threading.Tasks;
    using Models;

    public interface ICommentService
    {
        Task SetCommentToArticleAsync(Comment comment, Article article);
    }
}
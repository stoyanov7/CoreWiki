namespace CoreWiki.Domain.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface ICommentService
    {
        Task SetCommentToArticleAsync(Comment comment, Article article);
    }
}
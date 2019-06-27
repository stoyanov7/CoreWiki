namespace CoreWiki.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<TModel> FindBySlug<TModel>(string slug);

        Task Delete(string slug);
    }
}
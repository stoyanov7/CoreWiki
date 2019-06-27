namespace CoreWiki.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<TModel> FindBySlugAsync<TModel>(string slug);

        Task Delete(string slug);
    }
}
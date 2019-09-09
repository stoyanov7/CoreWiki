namespace CoreWiki.Domain.Services
{
    using System.Threading.Tasks;

    public interface IHaveIBeenPawnedClient
    {
        Task<int> GetHitsPlainAsync(string password);
    }
}
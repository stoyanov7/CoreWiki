namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;

    public interface IUserService
    {
        IEnumerable<TModel> GetAllUsers<TModel>();

        ICollection<string> GetAllRoleNames();
    }
}
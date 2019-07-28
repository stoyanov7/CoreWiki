namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        TModel FindByEmail<TModel>(string email);

        IEnumerable<TModel> GetAllUsers<TModel>();

        Task<bool> UpdateUserRolesAsync(string email, ICollection<string> roleNames, IEnumerable<string> updatedRoles);
    }
}
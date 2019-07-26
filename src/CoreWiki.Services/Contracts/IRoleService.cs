namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public interface IRoleService
    {
        ICollection<string> GetAllRoleNames();

        IEnumerable<IdentityRole> GetAllRoles();

        Task<IdentityResult> CreateNewRole(string roleName);
    }
}
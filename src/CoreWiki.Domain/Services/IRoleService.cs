namespace CoreWiki.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public interface IRoleService
    {
        Task<IdentityRole> FindByNameAsync(string roleToRemove);

        ICollection<string> GetAllRoleNames();

        IEnumerable<IdentityRole> GetAllRoles();

        Task<IdentityResult> CreateNewRole(string roleName);

        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
    }
}
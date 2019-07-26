namespace CoreWiki.Services.Contracts
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public interface IRoleService
    {
        ICollection<string> GetAllRoleNames();

        IEnumerable<IdentityRole> GetAllRoles();
    }
}
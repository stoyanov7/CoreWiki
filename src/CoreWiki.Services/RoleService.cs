namespace CoreWiki.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Microsoft.AspNetCore.Identity;

    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IEnumerable<IdentityRole> GetAllRoles() =>
            this.roleManager.Roles.ToList();

        public ICollection<string> GetAllRoleNames() =>
            this.roleManager
                .Roles
                .Select(x => x.Name)
                .ToList();
    }
}
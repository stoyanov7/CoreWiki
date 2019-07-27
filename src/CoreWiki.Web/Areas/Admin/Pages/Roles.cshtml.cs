namespace CoreWiki.Web.Areas.Admin.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class RolesModel : BaseModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRoleService roleService;

        public RolesModel(RoleManager<IdentityRole> roleManager, IRoleService roleService)
        {
            this.roleManager = roleManager;
            this.roleService = roleService;
        }

        [BindProperty]
        public string RoleToRemove { get; set; }

        public IEnumerable<IdentityRole> RolesList { get; private set; }

        public IActionResult OnGet()
        {
            this.RolesList = this.roleService.GetAllRoles();

            return this.Page();
        }

        public async Task<IActionResult> OnPostDeleteRoleAsync()
        {
            
            var role = await this.roleManager.FindByNameAsync(this.RoleToRemove);
            var result = await this.roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return this.RedirectToPage();
            }

            return this.Page();
        }
    }
}
namespace CoreWiki.Web.Areas.Admin.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class RolesModel : BaseModel
    {
        private readonly IRoleService roleService;

        public RolesModel(IRoleService roleService)
        {
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
            var role = await this.roleService.FindByNameAsync(this.RoleToRemove);
            var result = await this.roleService.DeleteRoleAsync(role);

            if (result.Succeeded)
            {
                return this.RedirectToPage();
            }

            return this.Page();
        }
    }
}
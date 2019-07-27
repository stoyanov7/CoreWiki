namespace CoreWiki.Web.Areas.Admin.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Identity;
    using Services.Contracts;

    public class UsersModel : BaseModel
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UsersModel(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;

            this.RoleNames = this.roleService.GetAllRoleNames();
        }

        public IEnumerable<ApplicationUser> UsersList { get; private set; }

        public ICollection<string> RoleNames { get; }

        [BindProperty]
        public IEnumerable<string> UpdatedRoles { get; set; }

        [BindProperty]
        public string UsernameToAddRoleTo { get; set; }

        public IActionResult OnGet()
        {
            this.UsersList = this.userService.GetAllUsers<ApplicationUser>();

            return this.Page();
        }

        public async Task<IActionResult> OnPostUpdateUserRolesAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var updateUserRoles = await this.userService
                .UpdateUserRolesAsync(this.UsernameToAddRoleTo, this.RoleNames, this.UpdatedRoles);

            if (updateUserRoles)
            {
                return this.RedirectToPage("Index");
            }
            else
            {
                return this.Page();
            }
        }
    }
}

namespace CoreWiki.Web.Areas.Admin.Pages
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.Identity;
    using Services.Contracts;

    public class UsersModel : PageModel
    {
        private readonly IUserService userService;

        public UsersModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<ApplicationUser> UsersList { get; private set; }

        public ICollection<string> RoleNames { get; private set; }

        [BindProperty]
        public string UsernameToAddRoleTo { get; set; }

        public IActionResult OnGet()
        {
            this.UsersList = this.userService.GetAllUsers<ApplicationUser>();
            this.RoleNames = this.userService.GetAllRoleNames();

            return this.Page();
        }
    }
}

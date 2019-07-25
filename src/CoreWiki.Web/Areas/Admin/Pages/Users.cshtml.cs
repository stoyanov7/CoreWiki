namespace CoreWiki.Web.Areas.Admin.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.Identity;
    using Services.Contracts;

    public class UsersModel : PageModel
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IEnumerable<ApplicationUser> UsersList { get; private set; }

        public List<IdentityRole> RolesList { get; private set; }

        public List<string> RoleNames { get; private set; }

        [BindProperty]
        public string UsernameToAddRoleTo { get; set; }

        public UsersModel(
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            
        }

        public IActionResult OnGet()
        {
            var currentRoles = this.roleManager.Roles.ToList();

            this.RolesList = currentRoles;
            this.UsersList = this.userService.GetAllUsers<ApplicationUser>();

            this.RoleNames = currentRoles.Select(r => r.Name).ToList();

            return this.Page();
        }
    }
}

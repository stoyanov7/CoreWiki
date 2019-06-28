namespace CoreWiki.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.Identity;

    public class MyRolesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public MyRolesModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ICollection<string> Roles { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user is null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.Roles = await this.userManager.GetRolesAsync(user);

            return this.Page();
        }
    }
}
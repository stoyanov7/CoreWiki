namespace CoreWiki.Web.Areas.Admin.Pages
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class AddModel : PageModel
    {
        private readonly IRoleService roleService;

        public AddModel(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [BindProperty]
        [Required]
        public string RoleName { get; set; }

        public void OnGet() => this.Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var result = await this.roleService.CreateNewRole(this.RoleName);

            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(error.Code, error.Description);
                }

                return this.Page();
            }

            return this.RedirectToPage("Index");
        }
    }
}
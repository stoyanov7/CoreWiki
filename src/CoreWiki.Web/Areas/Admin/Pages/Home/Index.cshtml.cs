namespace CoreWiki.Web.Areas.Admin.Pages.Home
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize("RequireAdministratorRole")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
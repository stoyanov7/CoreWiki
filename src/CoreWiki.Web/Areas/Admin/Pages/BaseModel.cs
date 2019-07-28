namespace CoreWiki.Web.Areas.Admin.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize("RequireAdministratorRole")]
    public class BaseModel : PageModel
    {
        
    }
}
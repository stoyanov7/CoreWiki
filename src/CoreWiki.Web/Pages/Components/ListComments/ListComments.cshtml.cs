namespace CoreWiki.Web.Pages.Components.ListComments
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    [ViewComponent(Name = "ListComments")]
    public class ListComments : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ICollection<Comment> comments)
        {
            return await Task.FromResult(this.View("ListComments", comments));
        }
    }
}

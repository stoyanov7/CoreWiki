namespace CoreWiki.Web.Pages.Components.ListComments
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;

    [ViewComponent(Name = "ListComments")]
    public class ListComments : ViewComponent
    {
        private readonly CoreWikiContext context;

        public ListComments(CoreWikiContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(ICollection<Comment> comments)
        {
            return this.View("ListComments", comments);
        }
    }
}

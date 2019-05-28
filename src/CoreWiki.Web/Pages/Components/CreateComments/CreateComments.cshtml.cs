namespace CoreWiki.Web.Pages.Components.CreateComments
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Data;
    using Models;

    [ViewComponent(Name = "CreateComments")]
    public class CreateComments : ViewComponent
    {
        private readonly CoreWikiContext context;

        public CreateComments(CoreWikiContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Comment comment)
        {
            return this.View("CreateComments", comment);
        }
    }
}

namespace CoreWiki.Web.Pages.Components.CreateComments
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
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
            return await Task.FromResult(this.View("CreateComments", comment));
        }
    }
}

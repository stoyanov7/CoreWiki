namespace CoreWiki.Web.Pages.Components.CreateComments
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [ViewComponent(Name = "CreateComments")]
    public class CreateComments : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Comment comment)
        {
            return await Task.FromResult(this.View("CreateComments", comment));
        }
    }
}

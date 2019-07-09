namespace CoreWiki.Web.Test
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Pages.Article;
    using Xunit;

    public class CreatePageTest
    {
        private readonly CreateModel model;

        public CreatePageTest()
        {
            this.model = new CreateModel(null, null, null);
        }

        [Fact]
        public void OnGet_ShouldBeOfTypePageResult()
        {
            var result = this.model.OnGet();

            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void OnGet_WithoutSlug_ShouldReturnPageResultWithNullArticle()
        {
            Assert.IsType<PageResult>(this.model.OnGet());
            Assert.Null(this.model.Article);
        }
    }
}

namespace CoreWiki.Web.Test
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Moq;
    using Pages.Article;
    using Repository.Contracts;
    using Xunit;

    public class CreatePageTest
    {
        private readonly CreateModel model;

        public CreatePageTest()
        {
            var articleRepositoryMock = new Mock<IArticleRepository>();
            this.model = new CreateModel(articleRepositoryMock.Object, null, null);
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

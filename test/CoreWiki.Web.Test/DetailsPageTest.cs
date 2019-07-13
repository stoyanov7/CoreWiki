namespace CoreWiki.Web.Test
{
    using System.Threading.Tasks;
    using Data;
    using Models;
    using Pages.Article;
    using Utilities;
    using Xunit;

    public class DetailsPageTest
    {
        private readonly CoreWikiContext context;
        private DetailsModel model;

        public DetailsPageTest()
        {
            this.context = CoreWikiContextMock.GetContext();
        }

        [Fact]
        public async Task OnGet_WithNullSlug_ShouldReturnArticleNotFoundResult()
        {
            this.model = new DetailsModel(null, null, null, null, null);
            
            Assert.IsType<ArticleNotFoundResult>(await this.model.OnGetAsync(null));
        }

        [Fact]
        public async Task OnGet_WithEmptySlug_ShouldReturnArticleNotFoundResult()
        {
            this.model = new DetailsModel(null, null, null, null, null);

            Assert.IsType<ArticleNotFoundResult>(await this.model.OnGetAsync(string.Empty));
        }
    }
}
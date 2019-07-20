namespace CoreWiki.Web.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Queries;
    using Data;
    using MediatR;
    using Models;
    using Moq;
    using Pages.Article;
    using Utilities;
    using Xunit;

    public class DetailsPageTest
    {
        private readonly CoreWikiContext context;
        private DetailsModel model;
        private readonly Mock<IMediator> mockMediator;

        public DetailsPageTest()
        {
            this.context = CoreWikiContextMock.GetContext();
            this.mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task OnGet_WithNullSlug_ShouldReturnArticleNotFoundResult()
        {
            this.model = new DetailsModel(null, null);
            
            Assert.IsType<ArticleNotFoundResult>(await this.model.OnGetAsync(null));
        }

        [Fact]
        public async Task OnGet_WithEmptySlug_ShouldReturnArticleNotFoundResult()
        {
            this.model = new DetailsModel(null, null);

            Assert.IsType<ArticleNotFoundResult>(await this.model.OnGetAsync(string.Empty));
        }

        [Fact]
        public async Task OnGet_WithValidSlug_ShouldReturnArticle()
        {
            var article = new Article
            {
                Topic = "test topic",
                Slug = "test-topic",
                Content = "test content"
            };

            this.context.Articles.Add(article);
            await this.context.SaveChangesAsync();

            this.mockMediator
                .Setup(m => m.Send(It.IsAny<GetArticleDetailsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(article);

            var detaisModel = new DetailsModel(this.mockMediator.Object, null);
            detaisModel.AddPageContext("", Guid.Empty.ToString());
            await detaisModel.OnGetAsync(article.Slug);

            Assert.Equal(article.Topic, detaisModel.Article.Topic);
            Assert.Equal(article.Slug, detaisModel.Article.Slug);
        }

        [Fact]
        public async Task OnGet_WithoutAnyArticle_ShouldReturnArticleNotFoundResult()
        {
            this.mockMediator
                .Setup(m => m.Send(It.IsAny<GetArticleDetailsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(default(Article)));

            var detaisModel = new DetailsModel(this.mockMediator.Object, null);
            
            Assert.Null(detaisModel.Article);
            Assert.IsType<ArticleNotFoundResult>(await detaisModel.OnGetAsync("test-slug"));
        }
    }
}
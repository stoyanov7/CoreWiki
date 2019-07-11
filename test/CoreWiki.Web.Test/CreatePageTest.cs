namespace CoreWiki.Web.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Commands;
    using Dto;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Moq;
    using Pages.Article;
    using Services.Contracts;
    using Xunit;

    public class CreatePageTest
    {
        private readonly CreateModel model;
        private readonly Mock<IArticleService> mockArticleService;
        private readonly Mock<IMediator> mockMediator;
        private readonly Mock<ILogger<CreateModel>> mockLogger;

        public CreatePageTest()
        {
            this.model = new CreateModel(null, null, null);
            this.mockArticleService = new Mock<IArticleService>();
            this.mockMediator = new Mock<IMediator>();
            this.mockLogger = new Mock<ILogger<CreateModel>>();
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

        [Fact]
        public async Task OnPost_CreateNewNonExistingArticle_ShouldCreateArticleAndRedirect()
        {
            var topic = "test topic";
            var content = "test content";
            var slug = "test-topic";
            var authorId = Guid.NewGuid().ToString();
            
            this.mockArticleService
                .Setup(x => x.IsArticleExist(topic));

            this.mockMediator
                .Setup(m => m.Send(It.IsAny<CreateNewArticleCommand>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(default(Unit)));

            var createModel = new CreateModel(this.mockMediator.Object, this.mockArticleService.Object, this.mockLogger.Object)
            {
                Article = new CreateArticleDto
                {
                    Topic = topic,
                    Content = content
                }
            };

            createModel.AddPageContext("username", authorId);
            var result = await createModel.OnPostAsync();

            Assert.IsType<RedirectResult>(result);
            Assert.Equal("./Index", ((RedirectResult)result).Url);
        }

    }
}

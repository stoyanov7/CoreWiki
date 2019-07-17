namespace CoreWiki.Web.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Commands;
    using Moq;
    using Services;
    using Services.Contracts;
    using Xunit;

    public class DeletePageTest
    {
        private readonly DeleteArticleCommandHandler deleteArticleCommandHandler;
        private readonly Mock<IArticleService> mockArticleService;
        private readonly DeleteArticleCommand deleteArticleCommand;

        private const string TestSlug = "test slug";

        public DeletePageTest()
        {
            this.mockArticleService = new Mock<IArticleService>();
            this.deleteArticleCommandHandler = new DeleteArticleCommandHandler(this.mockArticleService.Object);

            this.deleteArticleCommand = new DeleteArticleCommand(TestSlug);
        }

        [Fact]
        public async Task OnPost_TryToDeleteArticle_ShouldReturnSuccessfulResult()
        {
            var result = await this.deleteArticleCommandHandler
                .Handle(this.deleteArticleCommand, CancellationToken.None);

            this.mockArticleService.Verify(s => s.Delete(TestSlug));

            Assert.True(result.Successful);
        }

        [Fact]
        public async Task OnPost_TryToDeleteArticleUnsuccessful_ShouldTrowException()
        {
            var exception = new Exception();
            this.mockArticleService.Setup(s => s.Delete(TestSlug))
                .Throws(exception);

            var result = await this.deleteArticleCommandHandler
                .Handle(this.deleteArticleCommand, CancellationToken.None);

            Assert.False(result.Successful);
            Assert.Matches("There was an error deleting the article", result.Exception.Message);
            Assert.Same(exception, result.Exception.InnerException);
        }
    }
}
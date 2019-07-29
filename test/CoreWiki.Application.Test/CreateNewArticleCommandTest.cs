namespace CoreWiki.Application.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Moq;
    using Services.Contracts;
    using Xunit;

    public class CreateNewArticleCommandTest
    {
        private readonly Mock<IArticleService> mockArticleSerivce;
        private readonly CreateNewArticleCommandHandler createNewArticleCommandHandler;
        private CreateNewArticleCommand createNewArticleCommand;

        public CreateNewArticleCommandTest()
        {
            this.mockArticleSerivce = new Mock<IArticleService>();
            this.createNewArticleCommandHandler = new CreateNewArticleCommandHandler(this.mockArticleSerivce.Object, null);
        }

        [Fact]
        public async Task TryToCreateArticle_ShouldReturnSuccessfulResult()
        {
            this.createNewArticleCommand = new CreateNewArticleCommand("test topic", "test content", Guid.NewGuid().ToString());

            var result = await this.createNewArticleCommandHandler
                .Handle(this.createNewArticleCommand, CancellationToken.None);

            Assert.True(result.Successful, result.Exception?.Message);
        }
    }
}

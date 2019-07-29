namespace CoreWiki.Application.Test
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Domain.Repository;
    using Models;
    using Moq;
    using Xunit;

    public class EditArticleCommandTest
    {
        private readonly Mock<IArticleRepository> mockArticleRepository;
        private EditArticleCommand editArticleCommand;
        private readonly EditArticleCommandHandler editArticleCommandHandler;

        public EditArticleCommandTest()
        {
            this.mockArticleRepository = new Mock<IArticleRepository>();
            this.editArticleCommandHandler = new EditArticleCommandHandler(this.mockArticleRepository.Object);
        }

        [Fact]
        public async Task Handle_ArticleManagementServiceThrows_UnsuccessfulWithException()
        {
            var exception = new Exception();

            var article = new Article
            {
                Topic = "test topic"
            };

            this.mockArticleRepository
                .Setup(s => s.UpdateAsync(It.IsAny<Article>()))
                .Throws(exception);

            this.editArticleCommand = new EditArticleCommand(article);

            var result = await this.editArticleCommandHandler
                .Handle(this.editArticleCommand, CancellationToken.None);

            Assert.False(result.Successful);
            Assert.Same(exception, result.Exception);
        }
    }
}
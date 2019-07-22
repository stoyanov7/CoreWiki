namespace CoreWiki.Models.Test
{
    using Data.EntityConfiguration;
    using FluentValidation.TestHelper;
    using Xunit;

    public class ArticleShould
    {
        private readonly ArticleValidator configuration;

        public ArticleShould()
        {
            this.configuration = new ArticleValidator();
        }

        [Fact]
        public void Article_WithEmptyTopic_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Topic, "");
        }

        [Fact]
        public void Article_WithWhitespaceTopic_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Topic, new string(' ', 7));
        }

        [Fact]
        public void Article_WithNullTopic_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Topic, null as string);
        }

        [Fact]
        public void Article_WithVeryLongTopicLenght_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Topic, new string('a', 200));
        }

        [Fact]
        public void Article_WithMaximumLenghtTopic_ShouldNotThrowValidationError()
        {
            this.configuration.ShouldNotHaveValidationErrorFor(a => a.Topic, new string('a', 99));
        }
    }
}

namespace CoreWiki.Models.Test
{
    using Data.EntityConfiguration;
    using FluentValidation.TestHelper;
    using Xunit;

    public class CommentShould
    {
        private readonly CommentValidator configuration;

        public CommentShould()
        {
            this.configuration = new CommentValidator();
        }

        [Fact]
        public void Article_WithEmptyTopic_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Name, "");
        }

        [Fact]
        public void Article_WithWhitespaceTopic_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Name, new string(' ', 7));
        }

        [Fact]
        public void Article_WithNullTopic_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Name, null as string);
        }

        [Fact]
        public void Article_WithVeryLongTopicLenght_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(a => a.Name, new string('a', 200));
        }

        [Fact]
        public void Article_WithMaximumLenghtTopic_ShouldNotThrowValidationError()
        {
            this.configuration.ShouldNotHaveValidationErrorFor(a => a.Name, new string('a', 99));
        }

        [Fact]
        public void Article_WithInvalidEmailAddress_ShouldThrowValidationError()
        {
            this.configuration.ShouldHaveValidationErrorFor(e => e.Email, "test");
        }

        [Fact]
        public void Article_WithValidEmailAddress_ShouldNotThrowValidationError()
        {
            this.configuration.ShouldNotHaveValidationErrorFor(e => e.Email, "test@gmail.com");
        }
    }
}

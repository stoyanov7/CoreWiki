namespace CoreWiki.Utilities.Test
{
    using Xunit;

    public class UrlHelperTest
    {
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("One Two", "one-two")]
        [InlineData("Onetwo", "onetwo")]
        [InlineData("One Two Three", "one-two-three")]
        public void SlugShouldBeATopic(string slug, string expectedTopic)
        {
            var actualTopic = UrlHelpers.UrlFriendly(slug);

            Assert.Equal(expectedTopic, actualTopic);
        }
    }
}
namespace CoreWiki.Utilities.Test
{
    using Xunit;

    public class UrlHelperTests
    {
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("One Two", "one-two")]
        public void TopicToSlug(string topic, string expectedSlug)
        {
            var createdSlug = UrlHelpers.UrlFriendly(topic);

            Assert.Equal(expectedSlug, createdSlug);
        }
    }
}
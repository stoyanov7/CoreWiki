namespace CoreWiki.Utilities.Test
{
    using Xunit;

    public class StringHelperTest
    {
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData(" 12 34", 2)]
        [InlineData("Test", 1)]
        [InlineData(" Test", 1)]
        [InlineData(" Test ", 1)]
        [InlineData("  Test double space  ", 3)]
        [InlineData("Test double  space", 3)]
        [InlineData("Don't count \" spaced quotes \"", 4)]
        [InlineData("test content fair dinkum 12345", 5)]
        public void WordCountShouldBeAccurate(string sentence, int expectedWordCount)
        {
            var actualWordCount = sentence.WordCount();

            Assert.Equal(expectedWordCount, actualWordCount);
        }
    }
}

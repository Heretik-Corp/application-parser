using Xunit;

namespace ApplicationParser.Tests
{
    public class ArtifactDefTests
    {
        [Theory]
        [InlineData("%Hello", "PercentHello")]
        [InlineData("He%llo", "HePercentllo")]
        [InlineData("He)llo", "Hello")]
        [InlineData("He(llo", "Hello")]
        [InlineData("He&llo", "HeAndllo")]
        [InlineData("::Hello", "_Hello")]
        [InlineData("-Hello", "_Hello")]
        [InlineData("Hello,There", "HelloThere")]
        [InlineData("Hello/There", "HelloThere")]
        public void EncodeName_PassInNameWithBadChars_ReturnsCleanName(string test, string expected)
        {
            //ARRANGE

            //ACT
            var actual = ArtifactDef.EncodeName(test);

            //ASSERT
            Assert.Equal(actual, expected);

        }
    }
}


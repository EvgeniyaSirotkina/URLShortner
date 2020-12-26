using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;
using URLShortner.Service.Helpers;

namespace URLShortner.Service.Tests.Helpers
{
    public class UrlHelperTests
    {
        [Theory]
        [InlineData("http://test.com/")]
        [InlineData("https://test.com/")]
        [InlineData("HTTP://test.com/")]
        [InlineData("HTTPS://test.com/")]
        [InlineData("HttP://test.com/")]
        [InlineData("hTTpS://test.com/")]
        public void SupportsHTTProtocol_WhenContainValidProtocol_ShouldReturnTrue(string url)
        {
            // Arrange & Act
            var result = UrlHelper.SupportsHTTProtocol(url);

            // Assesrt
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("test.com/")]
        [InlineData("httpss://test.com/")]
        [InlineData("HTTPp://test.com/")]
        [InlineData("TTPS://test.com/")]
        [InlineData("HtP://test.com/")]
        [InlineData("hTTS://test.com/")]
        public void SupportsHTTProtocol_WhenDoesntContainProtocol_ShouldReturnFalse(string url)
        {
            // Arrange & Act
            var result = UrlHelper.SupportsHTTProtocol(url);

            // Assesrt
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("http://google.com/")]
        [InlineData("https://stackoverflow.com/")]
        [InlineData("google.com")]
        [InlineData("stackoverflow.com")]
        public void CheckUrlExists_WhenContainValidProtocol_ShouldReturnTrue(string url)
        {
            // Arrange & Act
            var result = url.CheckUrlExists();

            // Assesrt
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("test.com/")]
        [InlineData("http://test.com/")]
        [InlineData("https://test.com/")]
        [InlineData("HTTP://test.com/")]
        [InlineData("HTTPS://test.com/")]
        [InlineData("HttP://test.com/")]
        [InlineData("hTTpS://test.com/")]
        public void CheckUrlExists_WhenDoesntContainProtocol_ShouldReturnFalse(string url)
        {
            // Arrange & Act
            var result = url.CheckUrlExists();

            // Assesrt
            result.Should().BeFalse();
        }

        [Fact]
        public void GenerateShortUrl_ShouldReturnShortUrl()
        {
            // Arrange 
            var regex = new Regex(@"^([0-9a-z]{6})$");
            var length = 6;

            // Act
            var result = UrlHelper.GenerateShortUrl();

            // Assesrt
            var matches = regex.Matches(result);

            matches.Should().HaveCount(1);
            matches[0].Value.Should().Be(result);
            matches[0].Length.Should().Be(length);
        }
    }
}

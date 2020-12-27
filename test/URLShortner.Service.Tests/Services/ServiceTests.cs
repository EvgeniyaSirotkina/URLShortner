using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using URLShortner.Data.Interfaces;
using URLShortner.Service.Models;
using URLShortner.Service.Interfaces;
using URLShortner.Service.Services;
using URLShortner.Data.Models;

namespace URLShortner.Service.Tests.Services
{
    public class ServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepository> _mockRepository;

        private IUrlService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            _mockRepository = new Mock<IRepository>();

            _service = new UrlService(_mockRepository.Object);
        }

        [Fact]
        public async Task Create_WhenDataIsValid_ShouldReturnTrue()
        {
            // Arrange
            var newUrl = "https://google.com/";
            _mockRepository.Setup(x => x.Create(It.IsAny<Url>())).ReturnsAsync(true);

            // Act
            var result = await _service.Create(newUrl);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Create_WhenDataIsNullOrEmptyString_ShouldReturnFalse(string url)
        {
            // Arrange & Act
            var result = await _service.Create(url);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Create_WhenResourceDoesntExist_ShouldReturnFalse()
        {
            // Arrange
            var newUrl = "https://googl.com/";

            // Act
            var result = await _service.Create(newUrl);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Delete_WhenDataExist_ShouldReturnTrue()
        {
            // Arrange
            var urlId = 5;
            _mockRepository.Setup(x => x.Delete(urlId)).ReturnsAsync(true);

            // Act
            var result = await _service.Delete(urlId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_WhenIdIsNegativeNumber_ShouldReturnFalse()
        {
            // Arrange
            var urlId = -5;

            // Act
            var result = await _service.Delete(urlId);

            // Assert
            result.Should().BeFalse();
        }
    }
}

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
using AutoMapper;

namespace URLShortner.Service.Tests.Services
{
    public class ServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;

        private IUrlService _service;

        public ServiceTests()
        {
            _fixture = new Fixture();
            _mockRepository = new Mock<IRepository>();
            _mockMapper = new Mock<IMapper>();

            _service = new UrlService(_mockRepository.Object, _mockMapper.Object);
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

        [Fact]
        public void GetAll_ShouldReturnListOfUrlDtoObjects()
        {
            // Arrange
            var urls = new List<Url>
            {
                _fixture.Create<Url>(),
                _fixture.Create<Url>(),
            }.AsQueryable();
            var urlsList = urls.ToList();
            var urlsDto = new List<UrlDto>
            {
                _fixture.Build<UrlDto>()
                    .With(x => x.UrlId, urlsList[0].UrlId)
                    .With(x => x.LongUrl, urlsList[0].LongUrl)
                    .With(x => x.ShortUrl, urlsList[0].ShortUrl)
                    .With(x => x.Hits, urlsList[0].Hits)
                    .With(x => x.GeneratedDate, urlsList[0].GeneratedDate)
                    .Create(),
                _fixture.Build<UrlDto>()
                    .With(x => x.UrlId, urlsList[1].UrlId)
                    .With(x => x.LongUrl, urlsList[1].LongUrl)
                    .With(x => x.ShortUrl, urlsList[1].ShortUrl)
                    .With(x => x.Hits, urlsList[1].Hits)
                    .With(x => x.GeneratedDate, urlsList[1].GeneratedDate)
                    .Create(),
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(urls);
            _mockMapper.Setup(x => x.Map<IQueryable<Url>, IEnumerable<UrlDto>>(urls)).Returns(urlsDto);

            // Act
            var result = _service.GetAll();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(urlsList.Count);
            result.Should().BeEquivalentTo(new List<UrlDto>
            {
                new UrlDto
                {
                    UrlId = urlsList[0].UrlId,
                    LongUrl = urlsList[0].LongUrl,
                    ShortUrl = urlsList[0].ShortUrl,
                    Hits = urlsList[0].Hits,
                    GeneratedDate = urlsList[0].GeneratedDate,
                },
                new UrlDto
                {
                    UrlId = urlsList[1].UrlId,
                    LongUrl = urlsList[1].LongUrl,
                    ShortUrl = urlsList[1].ShortUrl,
                    Hits = urlsList[1].Hits,
                    GeneratedDate = urlsList[1].GeneratedDate,
                },
            });
        }

        [Fact]
        public async Task GetById_WhenDataExist_ShoulReturUrlDtoObject()
        {
            // Arrange
            var url = _fixture.Create<Url>();
            var urlDto = _fixture.Build<UrlDto>()
                    .With(x => x.UrlId, url.UrlId)
                    .With(x => x.LongUrl, url.LongUrl)
                    .With(x => x.ShortUrl, url.ShortUrl)
                    .With(x => x.Hits, url.Hits)
                    .With(x => x.GeneratedDate, url.GeneratedDate)
                    .Create();

            _mockRepository.Setup(x => x.GetById(urlDto.UrlId)).ReturnsAsync(url);
            _mockMapper.Setup(x => x.Map<Url, UrlDto>(url)).Returns(urlDto);

            // Act
            var result = await _service.GetById(urlDto.UrlId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new UrlDto
            {
                UrlId = url.UrlId,
                LongUrl = url.LongUrl,
                ShortUrl = url.ShortUrl,
                Hits = url.Hits,
                GeneratedDate = url.GeneratedDate,
            });
        }

        [Fact]
        public async Task GetById_WhenDataDoesntExist_ShoulReturNull()
        {
            // Arrange
            var urlDto = _fixture.Create<UrlDto>();

            _mockRepository.Setup(x => x.GetById(urlDto.UrlId)).ReturnsAsync(new Url());

            // Act
            var result = await _service.GetById(urlDto.UrlId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WhenIdIsNegativeNumber_ShoulReturNull()
        {
            // Arrange
            var id = -5;

            _mockRepository.Setup(x => x.GetById(id)).ReturnsAsync(new Url());

            // Act
            var result = await _service.GetById(id);

            // Assert
            result.Should().BeNull();
        }
    }
}

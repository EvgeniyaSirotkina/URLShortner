using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using URLShortner.Data.EF;
using URLShortner.Data.Models;
using URLShortner.Data.Repositories;

namespace URLShortner.Data.Tests
{
    public class RepositoryTests
    {
        private readonly IFixture _fixture;

        private UrlContext _context;

        public RepositoryTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Create_WhenDataIsValid_ShouldReturnTrue()
        {
            // Arrange
            var url = _fixture.Create<Url>();
            var repository = GetRepository();

            // Act
            var response = await repository.Create(url);

            // Assert
            var urls = repository.GetAll();
            urls.Should().HaveCount(4);

            response.Should().BeTrue();
        }

        [Fact]
        public async Task Create_WhenDataIsNull_ShouldReturnFalse()
        {
            // Arrange
            var repository = GetRepository();

            // Act
            var response = await repository.Create(null);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public async Task Delete_WhenDataExist_ShouldDeleteObjectAndReturnTrue()
        {
            // Arrange
            var url = _fixture.Create<Url>();
            var repository = GetRepository();
            await repository.Create(url);

            // Act
            var response = await repository.Delete(url.UrlId);

            // Assert
            var urls = repository.GetAll();
            urls.Should().HaveCount(3);

            response.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_WhenIdIsNegative_ShouldReturnFalse()
        {
            // Arrange
            var id = -3;
            var repository = GetRepository();

            // Act
            var response = await repository.Delete(id);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public async Task Delete_WhenDataDoesntExist_ShouldReturnFalse()
        {
            // Arrange
            var id = 4;
            var repository = GetRepository();

            // Act
            var response = await repository.Delete(id);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public void GetAll_ShouldReturnListOfUrls()
        {
            // Arrange
            var repository = GetRepository();

            // Act
            var response =  repository.GetAll();

            // Assert
            response.Should().NotBeNull();
            response.ToList().Should().HaveCount(3);
        }

        [Fact]
        public async Task Update_WhenDataExist_ShouldReturnTrue()
        {
            // Arrange
            var url = _fixture.Create<Url>();
            var repository = GetRepository();
            await repository.Create(url);
            url.Hits = 15;

            // Act
            var response = await repository.Update(url);

            // Assert
            response.Should().BeTrue();
        }

        [Fact]
        public async Task Update_WhenDataIsNull_ShouldReturnFalse()
        {
            // Arrange
            var repository = GetRepository();

            // Act
            var response = await repository.Update(null);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public async Task Update_WhenDataDoesntExist_ShouldReturnFalse()
        {
            // Arrange
            var url = _fixture.Create<Url>();
            var repository = GetRepository();

            // Act
            var response = await repository.Update(url);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public async Task GetById_WhenDataExist_ShouldReturnUrlObject()
        {
            // Arrange
            var url = _fixture.Create<Url>();
            var repository = GetRepository();
            await repository.Create(url);

            // Act
            var response = await repository.GetById(url.UrlId);

            // Assert
            response.Should().NotBeNull();
            response.Should().Be(url);
        }

        [Fact]
        public async Task GetById_WhenIdIsNegative_ShouldReturnNull()
        {
            // Arrange
            var id = -3;
            var repository = GetRepository();

            // Act
            var response = await repository.GetById(id);

            // Assert
            response.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WhenDataDoesntExist_ShouldReturnNull()
        {
            // Arrange
            var id = 4;
            var repository = GetRepository();

            // Act
            var response = await repository.GetById(id);

            // Assert
            response.Should().BeNull();
        }

        private Repository GetRepository()
        {
            _context = new UrlContext(GetOptions());

            SeedTestData();

            return new Repository(_context, Mock.Of<ILogger<Repository>>());
        }

        private DbContextOptions<UrlContext> GetOptions()
            => new DbContextOptionsBuilder<UrlContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

        private void SeedTestData()
        {
            _context.Urls.AddRange(
                new List<Url>
                {
                    _fixture.Build<Url>()
                        .With(x => x.UrlId, 1)
                        .With(x => x.LongUrl, "https://www.google.com/")
                        .With(x => x.ShortUrl, "https://short/cv8mol7")
                        .With(x => x.Hits, 0)
                        .Create(),
                    _fixture.Build<Url>()
                        .With(x => x.UrlId, 2)
                        .With(x => x.LongUrl, "https://github.com/")
                        .With(x => x.ShortUrl, "https://short/4dyt6b")
                        .With(x => x.Hits, 5)
                        .Create(),
                    _fixture.Build<Url>()
                        .With(x => x.UrlId, 3)
                        .With(x => x.LongUrl, "https://stackoverflow.com/")
                        .With(x => x.ShortUrl, "https://short/pmtltux")
                        .With(x => x.Hits, 2)
                        .Create(),
                });
            _context.SaveChanges();
        }
    }
}

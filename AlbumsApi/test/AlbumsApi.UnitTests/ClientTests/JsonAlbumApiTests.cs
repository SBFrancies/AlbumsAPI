using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Client.Client;
using AlbumsApi.Client.Interface;
using AlbumsApi.Client.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace AlbumsApi.UnitTests.ClientTests
{
    public class JsonAlbumApiTests
    {
        private readonly Mock<IHttpClient> _mockClient = new Mock<IHttpClient>();
        private readonly Fixture _fixture = new Fixture();

        public JsonAlbumApiTests()
        {
            _fixture.Customize(new AutoMoqCustomization());
            _mockClient.SetupGet(a => a.BaseUri).Returns(new Uri("http://test.com"));
        }

        [Fact]
        public async Task GetAllAlbumsReturnsAlbumsWhenAlbumsAreFound()
        {
            IEnumerable<AlbumEntity> albums = _fixture.CreateMany<AlbumEntity>();
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(albums), Encoding.UTF8, "application/json");

            Uri uri = new Uri(_mockClient.Object.BaseUri, "/albums");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.OK)
                .With(a => a.Content, stringContent)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<AlbumEntity> result = await sut.GetAllAlbumsAsync(default);

            Assert.NotNull(result);
            Assert.Equal(albums.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllAlbumsReturnsNullWhenNoAlbumsAreFound()
        {
            Uri uri = new Uri(_mockClient.Object.BaseUri, "/albums");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.NotFound)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<AlbumEntity> result = await sut.GetAllAlbumsAsync(default);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.AlreadyReported)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task GetAllAlbumsThrowsErrorWhenUnexpectedStatusCodeReturned(HttpStatusCode statusCode)
        {
            Uri uri = new Uri(_mockClient.Object.BaseUri, "/albums");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, statusCode)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<HttpRequestException>(() => sut.GetAllAlbumsAsync(default));
        }

        [Fact]
        public async Task GetAllPhotosReturnsAlbumsWhenPhotosAreFound()
        {
            IEnumerable<PhotoEntity> photos = _fixture.CreateMany<PhotoEntity>();
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(photos), Encoding.UTF8, "application/json");

            Uri uri = new Uri(_mockClient.Object.BaseUri, "/photos");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.OK)
                .With(a => a.Content, stringContent)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<PhotoEntity> result = await sut.GetAllPhotosAsync(default);

            Assert.NotNull(result);
            Assert.Equal(photos.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllPhotosReturnsNullWhenNoPhotosAreFound()
        {
            Uri uri = new Uri(_mockClient.Object.BaseUri, "/photos");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.NotFound)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<PhotoEntity> result = await sut.GetAllPhotosAsync(default);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.AlreadyReported)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task GetAllPhotosThrowsErrorWhenUnexpectedStatusCodeReturned(HttpStatusCode statusCode)
        {
            Uri uri = new Uri(_mockClient.Object.BaseUri, "/photos");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, statusCode)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<HttpRequestException>(() => sut.GetAllPhotosAsync(default));
        }

        [Fact]
        public async Task GetAllAlbumsForUserReturnsAlbumsWhenAlbumsAreFound()
        {
            int userId = _fixture.Create<int>();
            IEnumerable<AlbumEntity> albums = _fixture.CreateMany<AlbumEntity>();
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(albums), Encoding.UTF8, "application/json");

            Uri uri = new Uri(_mockClient.Object.BaseUri, $"/albums?userId={userId}");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.OK)
                .With(a => a.Content, stringContent)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<AlbumEntity> result = await sut.GetAlbumsForUserAsync(userId, default);

            Assert.NotNull(result);
            Assert.Equal(albums.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllAlbumsForUserReturnsNullWhenNoAlbumsAreFound()
        {
            int userId = _fixture.Create<int>();
            Uri uri = new Uri(_mockClient.Object.BaseUri, $"/albums?userId={userId}");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.NotFound)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<AlbumEntity> result = await sut.GetAlbumsForUserAsync(userId, default);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.AlreadyReported)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task GetAllAlbumsForUserThrowsErrorWhenUnexpectedStatusCodeReturned(HttpStatusCode statusCode)
        {
            int userId = _fixture.Create<int>();
            Uri uri = new Uri(_mockClient.Object.BaseUri, $"/albums?userId={userId}");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, statusCode)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<HttpRequestException>(() => sut.GetAlbumsForUserAsync(userId, default));
        }

        [Fact]
        public async Task GetAlbumPhotosReturnsAlbumsWhenPhotosAreFound()
        {
            int[] albumIds = _fixture.CreateMany<int>().ToArray();
            string query = string.Join('&', albumIds.Select(a => $"albumId={a}"));
            IEnumerable<PhotoEntity> photos = _fixture.CreateMany<PhotoEntity>();
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(photos), Encoding.UTF8, "application/json");

            Uri uri = new Uri(_mockClient.Object.BaseUri, $"/photos?{query}");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.OK)
                .With(a => a.Content, stringContent)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<PhotoEntity> result = await sut.GetPhotosInAlbumsAsync(albumIds, default);

            Assert.NotNull(result);
            Assert.Equal(photos.Count(), result.Count());
        }

        [Fact]
        public async Task GetAlbumPhotosReturnsNullWhenNoPhotosAreFound()
        {
            int[] albumIds = _fixture.CreateMany<int>().ToArray();
            string query = string.Join('&', albumIds.Select(a => $"albumId={a}"));
            Uri uri = new Uri(_mockClient.Object.BaseUri, $"/photos?{query}");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, HttpStatusCode.NotFound)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            IEnumerable<PhotoEntity> result = await sut.GetPhotosInAlbumsAsync(albumIds, default);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.AlreadyReported)]
        [InlineData(HttpStatusCode.Ambiguous)]
        public async Task GetAlbumPhotosThrowsErrorWhenUnexpectedStatusCodeReturned(HttpStatusCode statusCode)
        {
            int[] albumIds = _fixture.CreateMany<int>().ToArray();
            string query = string.Join('&', albumIds.Select(a => $"albumId={a}"));
            Uri uri = new Uri(_mockClient.Object.BaseUri, $"/photos?{query}");
            HttpResponseMessage response = _fixture.Build<HttpResponseMessage>()
                .With(a => a.StatusCode, statusCode)
                .Create();
            _mockClient.Setup(a => a.GetAsync(uri, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            JsonAlbumApi sut = CreateSystemUnderTest();

            await Assert.ThrowsAsync<HttpRequestException>(() => sut.GetPhotosInAlbumsAsync(albumIds, default));
        }

        private JsonAlbumApi CreateSystemUnderTest()
        {
            return new JsonAlbumApi(_mockClient.Object);
        }
    }
}

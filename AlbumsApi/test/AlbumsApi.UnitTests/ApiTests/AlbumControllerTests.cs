using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Api.Controllers;
using AlbumsApi.Common.Interface;
using AlbumsApi.Common.Models;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AlbumsApi.UnitTests.ApiTests
{
    public class AlbumControllerTests
    {
        private readonly Mock<IAlbumManager> _mockAlbumManager = new Mock<IAlbumManager>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task GetAsyncCallsAlbumManager()
        {
            AlbumsController sut = CreateSystemUnderTest();

            await sut.GetAsync(default);

            _mockAlbumManager.Verify(a => a.GetPhotoAlbumsAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetAsyncReturnsOkayWhenAlbumsFound()
        {
            IEnumerable<PhotoAlbum> albums = _fixture.CreateMany<PhotoAlbum>(10);
            _mockAlbumManager.Setup(a => a.GetPhotoAlbumsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(albums);
            AlbumsController sut = CreateSystemUnderTest();

            IActionResult response = await sut.GetAsync(default);

            _mockAlbumManager.Verify(a => a.GetPhotoAlbumsAsync(default), Times.Once);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task GetAsyncReturnsNotFoundWhenNoAlbumsFound()
        {
            _mockAlbumManager.Setup(a => a.GetPhotoAlbumsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<PhotoAlbum>());
            AlbumsController sut = CreateSystemUnderTest();

            IActionResult response = await sut.GetAsync(default);

            _mockAlbumManager.Verify(a => a.GetPhotoAlbumsAsync(default), Times.Once);

            Assert.IsType<NotFoundResult>(response);
        }


        [Fact]
        public async Task GetByUserAsyncCallsAlbumManager()
        {
            AlbumsController sut = CreateSystemUnderTest();
            int userId = _fixture.Create<int>();

            await sut.GetByUserAsync(userId, default);

            _mockAlbumManager.Verify(a => a.GetPhotoAlbumsForUserAsync(userId, default), Times.Once);
        }

        [Fact]
        public async Task GetByUserAsyncReturnsOkayWhenAlbumsFound()
        {
            IEnumerable<PhotoAlbum> albums = _fixture.CreateMany<PhotoAlbum>(10);
            int userId = _fixture.Create<int>();
            _mockAlbumManager.Setup(a => a.GetPhotoAlbumsForUserAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(albums);
            AlbumsController sut = CreateSystemUnderTest();

            IActionResult response = await sut.GetByUserAsync(userId, default);

            _mockAlbumManager.Verify(a => a.GetPhotoAlbumsForUserAsync(userId, default), Times.Once);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task GetByUserAsyncReturnsNotFoundWhenNoAlbumsFound()
        {
            int userId = _fixture.Create<int>();
            _mockAlbumManager.Setup(a => a.GetPhotoAlbumsForUserAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(new List<PhotoAlbum>());
            AlbumsController sut = CreateSystemUnderTest();

            IActionResult response = await sut.GetByUserAsync(userId, default);

            _mockAlbumManager.Verify(a => a.GetPhotoAlbumsForUserAsync(userId, default), Times.Once);

            Assert.IsType<NotFoundResult>(response);
        }


        private AlbumsController CreateSystemUnderTest()
        {
            return new AlbumsController(_mockAlbumManager.Object);
        }
    } 
}

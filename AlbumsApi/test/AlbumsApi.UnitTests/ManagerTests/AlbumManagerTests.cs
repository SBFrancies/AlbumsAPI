using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbumsApi.Client.Interface;
using AlbumsApi.Client.Models;
using AlbumsApi.Common.Models;
using AlbumsApi.Manager;
using AutoFixture;
using Moq;
using Xunit;

namespace AlbumsApi.UnitTests.ManagerTests
{
    public class AlbumManagerTests
    {
        private readonly Mock<IAlbumApi> _mockApi = new Mock<IAlbumApi>();
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task GetPhotoAlbumsCallsApiClient()
        {
            AlbumManager sut = CreateSystemUnderTest();

            await sut.GetPhotoAlbumsAsync(default);

            _mockApi.Verify(a => a.GetAllPhotosAsync(default), Times.Once);
            _mockApi.Verify(a => a.GetAllAlbumsAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetUserPhotoAlbumsCallsApiClient()
        {
            AlbumManager sut = CreateSystemUnderTest();
            int userId = _fixture.Create<int>();

            await sut.GetPhotoAlbumsForUserAsync(userId, default);

            _mockApi.Verify(a => a.GetAlbumsForUserAsync(userId, default), Times.Once);
            _mockApi.Verify(a => a.GetPhotosInAlbumsAsync(It.IsAny<int[]>(), default), Times.Once);
        }

        [Fact]
        public async Task CanCombineAlbumsAndPhotos()
        {
            int albumId = _fixture.Create<int>();
            AlbumEntity album = _fixture.Build<AlbumEntity>().With(a => a.Id, albumId).Create();
            List<AlbumEntity> albums = new List<AlbumEntity>{album};
            List<PhotoEntity> photos = _fixture.Build<PhotoEntity>().With(a => a.AlbumId, albumId).CreateMany().ToList();
            _mockApi.Setup(a => a.GetAllAlbumsAsync(default)).ReturnsAsync(albums);
            _mockApi.Setup(a => a.GetAllPhotosAsync(default)).ReturnsAsync(photos);

            AlbumManager sut = CreateSystemUnderTest();


            IEnumerable<PhotoAlbum> result = (await sut.GetPhotoAlbumsAsync(default)).ToList();

            Assert.Single(result);
            Assert.Equal(albumId, result.Single().Id);
            Assert.Equal(photos.Count, result.Single().Photos.Count());
        }

        private AlbumManager CreateSystemUnderTest()
        {
            return new AlbumManager(_mockApi.Object);
        }
    }
}

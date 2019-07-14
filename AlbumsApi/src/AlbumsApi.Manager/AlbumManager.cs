using AlbumsApi.Client.Interface;
using AlbumsApi.Common.Interface;
using AlbumsApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Client.Models;

namespace AlbumsApi.Manager
{
    public class AlbumManager : IAlbumManager
    {
        private readonly IAlbumApi _albumApi;

        public AlbumManager(IAlbumApi albumApi)
        {
            _albumApi = albumApi ?? throw new ArgumentNullException(nameof(albumApi));
        }

        public async Task<IEnumerable<PhotoAlbum>> GetPhotoAlbumsAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<AlbumEntity> albums = await _albumApi.GetAllAlbumsAsync(cancellationToken);
            IEnumerable<PhotoEntity> photos = await _albumApi.GetAllPhotosAsync(cancellationToken);

            return CombinePhotos(photos, albums);
        }

        public async Task<IEnumerable<PhotoAlbum>> GetPhotoAlbumsForUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            IList<AlbumEntity> albums = (await _albumApi.GetAlbumsForUserAsync(userId, cancellationToken)).ToList();
            IEnumerable<PhotoEntity> photos = await _albumApi.GetPhotosInAlbumsAsync(albums.Select(a => a.Id).ToArray(), cancellationToken);

            return CombinePhotos(photos, albums);
        }

        private IEnumerable<PhotoAlbum> CombinePhotos(IEnumerable<PhotoEntity> photos, IEnumerable<AlbumEntity> albums)
        {
            Dictionary<int, List<PhotoEntity>> photoDictionary = photos.GroupBy(a => a.AlbumId).ToDictionary(a => a.Key, a => a.ToList());

            return albums.Select(a => new PhotoAlbum
            {
                Id = a.Id,
                UserId = a.UserId,
                Title = a.Title,
                Photos = photoDictionary.ContainsKey(a.Id) ? photoDictionary[a.Id].Select(b => new Photo
                {
                    Id = b.Id,
                    Title = b.Title,
                    Url = b.Url,
                    ThumbnailUrl = b.ThumbnailUrl,
                }) : null,
            });
        }
    }
}

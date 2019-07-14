using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Client.Models;

namespace AlbumsApi.Client.Interface
{
    public interface IAlbumApi
    {
        Task<IEnumerable<AlbumEntity>> GetAllAlbumsAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<AlbumEntity>> GetAlbumsForUserAsync(int userId, CancellationToken cancellationToken = default);

        Task<IEnumerable<PhotoEntity>> GetAllPhotosAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<PhotoEntity>> GetPhotosInAlbumsAsync(int[] albumIds, CancellationToken cancellationToken = default);
    }
}

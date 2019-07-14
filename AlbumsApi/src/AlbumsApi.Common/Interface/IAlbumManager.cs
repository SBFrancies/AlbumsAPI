using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Common.Models;

namespace AlbumsApi.Common.Interface
{
    public interface IAlbumManager
    {
        Task<IEnumerable<PhotoAlbum>> GetPhotoAlbumsAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<PhotoAlbum>> GetPhotoAlbumsForUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Api.Filters;
using AlbumsApi.Common.Interface;
using AlbumsApi.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbumsApi.Api.Controllers
{
    [Route("v1/albums")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [GeneralExceptionFilter]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumManager _albumManager;

        public AlbumsController(IAlbumManager albumManager)
        {
            _albumManager = albumManager ?? throw new ArgumentNullException(nameof(albumManager));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PhotoAlbum>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<PhotoAlbum> albums = await _albumManager.GetPhotoAlbumsAsync(cancellationToken);

            if (albums == null || !albums.Any())
            {
                return NotFound();
            }

            return Ok(albums);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PhotoAlbum>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Route("{userId}")]
        public async Task<IActionResult> GetByUserAsync([FromRoute]int userId, CancellationToken cancellationToken = default)
        {
            IEnumerable<PhotoAlbum> albums = await _albumManager.GetPhotoAlbumsForUserAsync(userId, cancellationToken);

            if (albums == null || !albums.Any())
            {
                return NotFound();
            }

            return Ok(albums);
        }
    }
}
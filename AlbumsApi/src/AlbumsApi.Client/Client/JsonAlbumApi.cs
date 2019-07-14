using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Client.Interface;
using AlbumsApi.Client.Models;


namespace AlbumsApi.Client.Client
{
    public class JsonAlbumApi : IAlbumApi
    {
        private readonly IHttpClient _client;

        private Uri BaseUri => _client.BaseUri;

        public JsonAlbumApi(IHttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<AlbumEntity>> GetAllAlbumsAsync(CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, "/albums");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<IEnumerable<AlbumEntity>>(cancellationToken);

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<IEnumerable<AlbumEntity>> GetAlbumsForUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, $"/albums?userId={userId}");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<IEnumerable<AlbumEntity>>(cancellationToken);

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<IEnumerable<PhotoEntity>> GetAllPhotosAsync(CancellationToken cancellationToken = default)
        {
            Uri uri = new Uri(BaseUri, "/photos");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<IEnumerable<PhotoEntity>>(cancellationToken);

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }

        public async Task<IEnumerable<PhotoEntity>> GetPhotosInAlbumsAsync(int[] albumIds, CancellationToken cancellationToken = default)
        {
            if (albumIds == null)
            {
                throw  new ArgumentNullException(nameof(albumIds));
            }

            string query = string.Join('&', albumIds.Select(a => $"albumId={a}"));
            Uri uri = new Uri(BaseUri, $"/photos?{query}");
            HttpResponseMessage result = await _client.GetAsync(uri, cancellationToken);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await result.Content.ReadAsAsync<IEnumerable<PhotoEntity>>(cancellationToken);

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HttpRequestException($"Unexpected HTTP Status Code: {result.StatusCode}");
            }
        }
    }
}

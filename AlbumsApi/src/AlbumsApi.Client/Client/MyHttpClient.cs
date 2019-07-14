using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlbumsApi.Client.Interface;

namespace AlbumsApi.Client.Client
{
    public class MyHttpClient : IHttpClient
    {
        private readonly HttpClient _client = new HttpClient();

        public Uri BaseUri { get; }

        public MyHttpClient(string baseUri)
        {
            BaseUri = new Uri(baseUri);
        }

        public async Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            return await _client.GetAsync(uri, cancellationToken);
        }
    }
}

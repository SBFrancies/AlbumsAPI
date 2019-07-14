using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumsApi.Client.Interface
{
    public interface IHttpClient
    {
        Uri BaseUri { get; }

        Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken cancellationToken = default);
    }
}

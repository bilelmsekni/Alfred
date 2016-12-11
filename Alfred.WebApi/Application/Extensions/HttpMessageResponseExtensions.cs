using System.Net.Http;

namespace Alfred.WebApi.Application.Extensions
{
    public static class HttpMessageResponseExtensions
    {
        public static int GetStatusCode(this HttpResponseMessage response)
        {
            return response != null ? (int)response.StatusCode : 0;
        }
    }
}

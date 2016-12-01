using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Alfred.Shared.Constants;

namespace Alfred.Shared.Tests
{
    public class FakeHttpMessageBuilder
    {
        public static HttpRequestMessage CreateFakeHttpMessage(string uri = "http://localhost:3000", string route = AlfredRoutes.GetArtifacts)
        {
            return new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Properties = {
                    {
                        HttpPropertyKeys.HttpConfigurationKey,
                        new HttpConfiguration { Routes = {{ route, new HttpRoute() }} }
                    }
                }
            };
        }
    }
}
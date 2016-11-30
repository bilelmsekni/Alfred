using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alfred.IoC.HttpRequestMessage;
using LightInject;

namespace Alfred.IoC.Handlers
{
    public class HttpRequestMessageHandler : DelegatingHandler
    {
        private readonly LogicalThreadStorage<HttpRequestMessageStorage> _messageStorage =
            new LogicalThreadStorage<HttpRequestMessageStorage>(() => new HttpRequestMessageStorage());

        protected override Task<HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _messageStorage.Value.Message = request;
            return base.SendAsync(request, cancellationToken);
        }

        public System.Net.Http.HttpRequestMessage GetCurrentMessage()
        {
            return _messageStorage.Value.Message;
        }
    }
}

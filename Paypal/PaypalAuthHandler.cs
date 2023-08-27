using Polly;
using Polly.Retry;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PaypalDemo.Paypal
{
    public class PaypalAuthHandler : DelegatingHandler
    {
        private readonly IAuthTokenProvider _authTokenProvider;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        public PaypalAuthHandler(IAuthTokenProvider authTokenProvider)
        {
            _authTokenProvider = authTokenProvider;

            _retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => (new HttpStatusCode[]
            {
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
            }).Contains(r.StatusCode))
            .RetryAsync((_, _) => _authTokenProvider.RefreshToken());
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await _retryPolicy.ExecuteAsync(async _ =>
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _authTokenProvider.GetAccessToken());
                return await base.SendAsync(request, cancellationToken);

            }, cancellationToken);
        }
    }
}

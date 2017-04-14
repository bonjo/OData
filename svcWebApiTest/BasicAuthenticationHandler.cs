using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace VgFc.Test.Services
{
    /// <summary>
    /// To manage the Security of the service
    /// </summary>
    public class BasicAuthenticationHandler:DelegatingHandler
    {
        private readonly IAuthenticationService _service;

        public BasicAuthenticationHandler(IAuthenticationService service)
        {
            _service = service;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue autHeader = request.Headers.Authorization;
            if (autHeader == null || autHeader.Scheme != "Basic")
                return Unauthorized(request);
            // Decode request credentials
            string requestCredentials = autHeader.Parameter;
            byte[] credentialBytes = Convert.FromBase64String(requestCredentials);
            string[] credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');
            // send credentials[0] and credentials[1] to authentication system
            _service.Authenticate(credentials[0], credentials[1]);
            // I asume that the authentication process was success, so create information to use whit ASP.Net membership
            string[] roles = null;
            IIdentity identity = new GenericIdentity(credentials[0], "Basic");
            IPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;

            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Method to response an unauthorized access, I need to add a header to ask the cliente for the credentials
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<HttpResponseMessage> Unauthorized(HttpRequestMessage request)
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Headers.Add("WWW-Authenticate", "Basic");
            TaskCompletionSource<HttpResponseMessage> task = new TaskCompletionSource<HttpResponseMessage>();
            task.SetResult(response);

            return task.Task;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkaffolderTemplate.Support
{
    public class CustomDelegatingHandlerTokenRefresher : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
             {
                 var response = task.Result;
                 if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                     MessagingCenter.Send<CustomDelegatingHandlerTokenRefresher, bool>(this, Events.TokenExpired, true);
                 return response;
             });
        }
    }
}

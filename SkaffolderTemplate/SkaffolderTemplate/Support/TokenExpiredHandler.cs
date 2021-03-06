﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkaffolderTemplate.Support
{
    public class TokenExpiredHandler : DelegatingHandler
    {
        public TokenExpiredHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            if (content.Equals("No Token Provided"))
                MessagingCenter.Send<TokenExpiredHandler, bool>(this, Events.TokenExpired, true);
            return response;
        }
    }
}

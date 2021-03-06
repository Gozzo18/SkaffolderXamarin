﻿using SkaffolderTemplate.Support;
using System;
using System.Net.Http;

namespace SkaffolderTemplate.Rest
{
    public abstract class RestClient
    {
        public static HttpClient client { get; set; } = new HttpClient(new TokenExpiredHandler(new HttpClientHandler()));
        
        public RestClient()
        {
            client.BaseAddress = new Uri("http://192.168.140.73:3000/api/");
            client.MaxResponseContentBufferSize = 256000;
        }
    }
}

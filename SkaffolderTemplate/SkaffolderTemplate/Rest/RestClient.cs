using System.Net.Http;

namespace SkaffolderTemplate.Rest
{
    public abstract class RestClient
    {
        public static HttpClient client { get; set; } = new HttpClient();

        public RestClient()
        {
            client.MaxResponseContentBufferSize = 256000;
        }
    }
}

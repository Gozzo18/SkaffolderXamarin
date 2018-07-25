using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace SkaffolderTemplate.Rest.Security
{
    public class LoginRestService
    {
        HttpClient client;

        public LoginRestService()
        {
            client = new HttpClient();
        }

        //LOGIN
        public async Task<bool> LoginAsync(string username, string password)
        {
            bool isPresent = false;

            try
            {
                //Password encrypted
                String sb = encryptPassword(password);

                var json = $"{{\"username\":\"{username}\",\"password\":\"{sb}\"}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(App.LOGIN_URL, content);
           
                //If data are correct...
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    isPresent = true;

                    //Extract the token and the id from response body
                    var data = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    var token = data.SelectToken("token").ToString();
                    var id = data.SelectToken("_id").ToString();

                    //Set the authorization type of client
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    //Save the token and the id permanently
                    var app = App.Current as App;
                    app.AuthenticationToken = token;
                    app.UserId = id;
                    await app.SavePropertiesAsync();
                }
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
            return isPresent;
        }

        //VERIFY TOKEN
        public async Task<bool> VerifyToken(string token)
        {
            bool tokenPresent = false;

            if (token != "")
            {
                try
                {
                    var json = $"{{\"token\":\"{token}\"}}";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(App.VERIFY_TOKEN_URL, content);
                    if (response.IsSuccessStatusCode)
                        tokenPresent = true;
                }catch (Exception e){
                    Debug.WriteLine(@"				ERROR{0}", e);
                }
            }
            return tokenPresent;
        }

        private String encryptPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2").ToLower());
            }
            return sb.ToString();
        }
    }
}

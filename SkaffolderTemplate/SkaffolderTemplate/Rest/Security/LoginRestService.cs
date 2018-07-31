using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using SkaffolderTemplate.Models;

namespace SkaffolderTemplate.Rest.Security
{
    public class LoginRestService : RestClient
    {
        //LOGIN
        /// <summary>
        /// Login procedure
        /// </summary>
        /// <param name="username">Username inserted by user</param>
        /// <param name="password">Password inserted by user</param>
        /// <returns>TRUE if HttpResonse is successful (code 200), FALSE otherwise</returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            bool isPresent = false;

            try
            {
                //Password encrypted
                String sb = EncryptPassword(password);

                var json = $"{{\"username\":\"{username}\",\"password\":\"{sb}\"}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(App.LOGIN_URL, content);
           
                //If data are correct...
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    isPresent = true;

                    //Extract the token and the id from response body
                    var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

                    //Set the authorization type of client
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

                    //Save token, id, password and current user permanently
                    var app = App.Current as App;
                    app.AuthenticationToken = user.Token;
                    app.UserId = user.Id;
                    app.Password = password;
                    app.CurrentUserRole = user.Roles[0];
                    await app.SavePropertiesAsync();
                }
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
            return isPresent;
        }

        //VERIFY TOKEN
        /// <summary>
        /// Check if token is still valid or present.
        /// </summary>
        /// <param name="token">Token to check</param>
        /// <returns>TRUE if token is valid, FALSE otherwise</returns>
        public async Task<bool> VerifyToken(string token)
        {
            bool tokenPresent = false;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var json = $"{{\"token\":\"{token}\"}}";
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(App.VERIFY_TOKEN_URL, content);
                    if (response.IsSuccessStatusCode)
                    {
                        tokenPresent = true;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }catch (Exception e){
                    Debug.WriteLine(@"				ERROR{0}", e);
                }
            }
            return tokenPresent;
        }

        //CHANGE PASSWORD
        /// <summary>
        /// Change password procedure
        /// </summary>
        /// <param name="oldPassword">Password to change</param>
        /// <param name="newPassowrd">New Password to set</param>
        /// <returns>TRUE if password is correctly changed, FALSE otherwise</returns>
        public async Task<bool> ChangePassword(string oldPassword, string newPassowrd)
        {
            String oldPass = EncryptPassword(oldPassword);
            String newPass = EncryptPassword(newPassowrd);
            bool correctChange = false;
            try
            {
                var json = $"{{\"passwordNew\":\"{newPass}\",\"passwordOld\":\"{oldPass}\"}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.CHANGE_PASSWORD_URL, content);
                Debug.WriteLine("Success : " + response.IsSuccessStatusCode);
                correctChange = true;
            }catch(Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
            return correctChange;
        }

        /// <summary>
        /// Encrypting method using MD5 algorithm
        /// </summary>
        /// <param name="password">Password to encrypt</param>
        /// <returns>Encrypted password</returns>
        public String EncryptPassword(string password)
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

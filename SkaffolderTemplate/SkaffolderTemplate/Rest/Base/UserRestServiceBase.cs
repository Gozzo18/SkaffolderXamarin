﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkaffolderTemplate.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkaffolderTemplate.Rest.Base
{
    public class UserRestServiceBase
    {
        HttpClient client;
        ObservableCollection<User> _users { get; set; }

        public UserRestServiceBase()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        //DELETE
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id">Id of the User to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            var app = Application.Current as App;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", app.AuthenticationToken);

            try
            {
                var response = await client.DeleteAsync(App.USER_URL + id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //POST
        /// <summary>
        /// Add a new User
        /// </summary>
        /// <param name="item">User to Add</param>
        /// <returns>void</returns>
        public async Task POST(User item)
        {
            var app = Application.Current as App;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", app.AuthenticationToken);

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.USER_URL, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of an User
        /// </summary>
        /// <param name="item">User to update</param>
        /// <returns></returns>
        public async Task PUT(User item)
        {
            var app = Application.Current as App;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", app.AuthenticationToken);
            Debug.WriteLine("autorizzazzioni : " + client.DefaultRequestHeaders.Authorization);
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.USER_URL + item.Id, content);
                Debug.WriteLine("succes : " + response.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of Users
        /// </summary>
        /// <returns>User list</returns>
        public async Task<ObservableCollection<User>> GETList()
        {
            _users = new ObservableCollection<User>();
            var uri = new Uri(String.Format(App.USER_URL, string.Empty));
            var app = Application.Current as App;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", app.AuthenticationToken);

            try
            {
                var content = await client.GetStringAsync(uri);
                _users = JsonConvert.DeserializeObject<ObservableCollection<User>>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return _users;
        }

        //GET ID
        /// <summary>
        /// Get a User  
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GETId(string userId)
        {
            User user = new User();
            var app = Application.Current as App;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", app.AuthenticationToken);

            try
            {
                var content = await client.GetStringAsync(App.USER_URL + userId);
                user = JsonConvert.DeserializeObject<User>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                }
            return user;
        }
    }
}
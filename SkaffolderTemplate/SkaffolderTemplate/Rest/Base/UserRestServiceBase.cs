using Newtonsoft.Json;
using SkaffolderTemplate.Models;
using SkaffolderTemplate.Support;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkaffolderTemplate.Rest.Base
{
    public class UserRestServiceBase : RestClient
    {
        private const string UserApi ="users/";
        public ObservableCollection<User> _userlist { get; private set; }

        //DELETE
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id">Id of the User to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(UserApi + id);
            }catch (Exception e){
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
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(UserApi, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of a User
        /// </summary>
        /// <param name="item">User to Update</param>
        /// <returns></returns>
        public async Task PUT(User item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(UserApi + item.Id, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of Users
        /// </summary>
        /// <returns>User List</returns>
        public async Task<ObservableCollection<User>> GETList()
        {
            _user = new ObservableCollection<User>();
            try
            {
                var content = await client.GetStringAsync(UserApi);
                _user = JsonConvert.DeserializeObject<ObservableCollection<User>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                //Send a notify of token expiration, to whoever is subscribed to this RestService
                MessagingCenter.Send<UserRestServiceBase, bool>(this, Events.TokenExpired, true);
            }
            return _user;
        }

        //GET ID
        /// <summary>
        /// Get a User
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GETId(string userId)
        {
            User user = new User();
            try
            {
                var content = await client.GetStringAsync(UserApi + filmId);
                user = JsonConvert.DeserializeObject<User>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return user;
        }
    }
}

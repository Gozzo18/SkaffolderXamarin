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
    public class ActorRestServiceBase : RestClient
    {
        ObservableCollection<Actor> _actors { get; set; }

        //DELETE
        /// <summary>
        /// Delete an Actor
        /// </summary>
        /// <param name="id">Id of the Actor to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(App.ACTOR_URL + id);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //POST
        /// <summary>
        /// Add a new Actor
        /// </summary>
        /// <param name="item">Actor to Add</param>
        /// <returns>void</returns>
        public async Task POST(Actor item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.ACTOR_URL, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of an Actor
        /// </summary>
        /// <param name="item">Actor to update</param>
        /// <returns></returns>
        public async Task PUT(Actor item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.ACTOR_URL + item.Id, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of Actors
        /// </summary>
        /// <returns>Actor list</returns>
        public async Task<ObservableCollection<Actor>> GETList()
        {
            _actors = new ObservableCollection<Actor>();
            try
            {
                var content = await client.GetStringAsync(App.ACTOR_URL);
                _actors = JsonConvert.DeserializeObject<ObservableCollection<Actor>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                //Send a notification of token expiration, to whoever is subscribed to this RestService
                MessagingCenter.Send<ActorRestServiceBase, bool>(this, Events.TokenExpired, true);
            }
            return _actors;
        }

        //GET ID
        /// <summary>
        /// Get an Actor  
        /// </summary>
        /// <returns>Actor with that Id</returns>
        public async Task<Actor> GETId(string actorId)
        {
            Actor actor = new Actor();
            try
            {
                var content = await client.GetStringAsync(App.ACTOR_URL + actorId);
                actor = JsonConvert.DeserializeObject<Actor>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return actor;
        }
    }
}

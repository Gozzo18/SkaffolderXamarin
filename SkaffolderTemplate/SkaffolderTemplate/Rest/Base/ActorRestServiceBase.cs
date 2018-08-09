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
        private const string ActorApi ="actors/";
        public ObservableCollection<Actor> _actorlist { get; private set; }

        //DELETE
        /// <summary>
        /// Delete a Actor
        /// </summary>
        /// <param name="id">Id of the Actor to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(ActorApi + id);
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
                HttpResponseMessage response = await client.PostAsync(ActorApi, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of a Actor
        /// </summary>
        /// <param name="item">Actor to Update</param>
        /// <returns></returns>
        public async Task PUT(Actor item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ActorApi + item.Id, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of Actors
        /// </summary>
        /// <returns>Actor List</returns>
        public async Task<ObservableCollection<Actor>> GETList()
        {
            _actorlist = new ObservableCollection<Actor>();
            try
            {
                var content = await client.GetStringAsync(ActorApi);
                _actorlist = JsonConvert.DeserializeObject<ObservableCollection<Actor>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return _actorlist;
        }

        //GET ID
        /// <summary>
        /// Get a Actor
        /// </summary>
        /// <returns>Actor</returns>
        public async Task<Actor> GETId(string actorId)
        {
            Actor actor = new Actor();
            try
            {
                var content = await client.GetStringAsync(ActorApi + actorId);
                actor = JsonConvert.DeserializeObject<Actor>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return actor;
        }
    }
}

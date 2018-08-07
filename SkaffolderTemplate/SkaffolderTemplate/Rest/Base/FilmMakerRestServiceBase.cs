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
    public class FilmMakerRestServiceBase : RestClient
    {
        private const string FilmMakerApi ="filmmakers/";
        public ObservableCollection<FilmMaker> _filmmakerlist { get; private set; }

        //DELETE
        /// <summary>
        /// Delete a FilmMaker
        /// </summary>
        /// <param name="id">Id of the FilmMaker to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(FilmMakerApi + id);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //POST
        /// <summary>
        /// Add a new FilmMaker
        /// </summary>
        /// <param name="item">FilmMaker to Add</param>
        /// <returns>void</returns>
        public async Task POST(FilmMaker item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(FilmMakerApi, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of a FilmMaker
        /// </summary>
        /// <param name="item">FilmMaker to Update</param>
        /// <returns></returns>
        public async Task PUT(FilmMaker item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(FilmMakerApi + item.Id, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of FilmMakers
        /// </summary>
        /// <returns>FilmMaker List</returns>
        public async Task<ObservableCollection<FilmMaker>> GETList()
        {
            _filmmakerlist = new ObservableCollection<FilmMaker>();
            try
            {
                var content = await client.GetStringAsync(FilmMakerApi);
                _filmmakerlist = JsonConvert.DeserializeObject<ObservableCollection<FilmMaker>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                //Send a notify of token expiration, to whoever is subscribed to this RestService
                MessagingCenter.Send<FilmMakerRestServiceBase, bool>(this, Events.TokenExpired, true);
            }
            return _filmmakerlist;
        }

        //GET ID
        /// <summary>
        /// Get a FilmMaker
        /// </summary>
        /// <returns>FilmMaker</returns>
        public async Task<FilmMaker> GETId(string filmmakerId)
        {
            FilmMaker filmmaker = new FilmMaker();
            try
            {
                var content = await client.GetStringAsync(FilmMakerApi + filmmakerId);
                filmmaker = JsonConvert.DeserializeObject<FilmMaker>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return filmmaker;
        }
    }
}

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
        ObservableCollection<FilmMaker> _filmMakers { get; set; }

        //DELETE
        /// <summary>
        /// Delete a Film Maker
        /// </summary>
        /// <param name="id">Id of the Film Maker to Delete</param>
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
        /// Add a new Film Maker
        /// </summary>
        /// <param name="item">Film maker to Add</param>
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
        /// Update info of a Film Maker
        /// </summary>
        /// <param name="item">Film-Maker da modificare</param>
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
        /// Get the complete list of Film Makers
        /// </summary>
        /// <returns>Lista di attori</returns>
        public async Task<ObservableCollection<FilmMaker>> GETList()
        {
            _filmMakers = new ObservableCollection<FilmMaker>();
            try
            {
                var content = await client.GetStringAsync(FilmMakerApi);
                _filmMakers = JsonConvert.DeserializeObject<ObservableCollection<FilmMaker>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                MessagingCenter.Send<FilmMakerRestServiceBase, bool>(this, Events.TokenExpired, true);
            }
            return _filmMakers;
        }

        //GET ID
        /// <summary>
        /// Get a Film Maker
        /// </summary>
        /// <returns>Film Maker</returns>
        public async Task<FilmMaker> GETId(string filmMakerId)
        {
            FilmMaker filmMaker = new FilmMaker();
            try
            {
                var content = await client.GetStringAsync(FilmMakerApi + filmMakerId);
                filmMaker = JsonConvert.DeserializeObject<FilmMaker>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return filmMaker;          
        }
    }
}

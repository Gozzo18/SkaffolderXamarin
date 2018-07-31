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
    public class FilmRestServiceBase : RestClient
    {
        public ObservableCollection<Film> _listaDiFilm { get; private set; }

        //DELETE
        /// <summary>
        /// Delete a Film
        /// </summary>
        /// <param name="id">Id of the Film to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(App.FILM_URL + id);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //POST
        /// <summary>
        /// Add a new Film
        /// </summary>
        /// <param name="item">Film to Add</param>
        /// <returns>void</returns>
        public async Task POST(Film item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.FILM_URL, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of a Film
        /// </summary>
        /// <param name="item">Film to Update</param>
        /// <returns></returns>
        public async Task PUT(Film item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.FILM_URL + item.Id, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of Films
        /// </summary>
        /// <returns>Film List</returns>
        public async Task<ObservableCollection<Film>> GETList()
        {
            _listaDiFilm = new ObservableCollection<Film>();
            try
            {
                var content = await client.GetStringAsync(App.FILM_URL);
                _listaDiFilm = JsonConvert.DeserializeObject<ObservableCollection<Film>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                MessagingCenter.Send<FilmRestServiceBase, bool>(this, Events.TokenExpired, true);
            }
            return _listaDiFilm;
        }

        //GET ID
        /// <summary>
        /// Get a Film
        /// </summary>
        /// <returns>Film</returns>
        public async Task<Film> GETId(string filmId)
        {
            Film film = new Film();

            try
            {
                var content = await client.GetStringAsync(App.FILM_URL + filmId);
                film = JsonConvert.DeserializeObject<Film>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return film;
        }
    }
}

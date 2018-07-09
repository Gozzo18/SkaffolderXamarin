using Newtonsoft.Json;
using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkaffolderTemplate.Rest
{
    public class FilmRestService
    {
        HttpClient client;
        public List<Film> ListaDiFilm { get; private set; }

        public FilmRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        //DELETE
        /// <summary>
        /// Cancella un film
        /// </summary>
        /// <param name="id">Id del film da cancellare</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(App.FILM_URL + id);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Film successfully deleted.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Ottieni la lista di film memorizzati
        /// </summary>
        /// <returns>Lista di film</returns>
        public async Task<List<Film>> GETList()
        {
            ListaDiFilm = new List<Film>();
            var uri = new Uri(String.Format(App.FILM_URL, string.Empty));

            try
            {
                var content = await client.GetStringAsync(uri);
                ListaDiFilm = JsonConvert.DeserializeObject<List<Film>>(content);

            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return ListaDiFilm;
        }


        //GET ID
        /// <summary>
        /// Ottieni il film a partire dall'id
        /// </summary>
        /// <returns>Film</returns>
        public async Task<Film> GETId(string filmId)
        {
            Film film = new Film();

            try
            {
                var content = await client.GetStringAsync(App.FILM_URL + filmId);
                film = JsonConvert.DeserializeObject<Film>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return film;
        }

        //POST
        /// <summary>
        /// Inserisce un film
        /// </summary>
        /// <param name="item">Film da inserire</param>
        /// <returns>void</returns>
        public async Task POST(Film item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(App.FILM_URL, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Film successfully saved.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Modifica un film già presente
        /// </summary>
        /// <param name="item">Film da modificare</param>
        /// <returns></returns>
        public async Task PUT(Film item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.FILM_URL + item._id, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Film successfully modified.");
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}

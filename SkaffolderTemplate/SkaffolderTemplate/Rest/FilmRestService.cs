using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

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
        public async Task DeleteFilmAsync(string id)
        {
            var uri = new Uri(String.Format(App.FILM_URL + id, string.Empty));

            try
            {
                var response = await client.DeleteAsync(uri);

                if(response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Film successfully deleted.");
            } catch(Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Ottieni la lista di film memorizzati
        /// </summary>
        /// <returns>Lista di film</returns>
        public async Task<List<Film>> RefreshDataAsync()
        {
            ListaDiFilm = new List<Film>();

            var uri = new Uri(String.Format(App.FILM_URL, string.Empty));

            try
            {
                    //var content = await response.Content.ReadAsStringAsync();
                    var content = await client.GetStringAsync(uri);
                    ListaDiFilm = JsonConvert.DeserializeObject<List<Film>>(content);
                
            } catch(Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return ListaDiFilm;
        }
        
        //POST
        /// <summary>
        /// Inserisce/Aggiorna un film
        /// </summary>
        /// <param name="item">Film da inserire o aggiornare</param>
        /// <param name="isNew">Inserire allora true, Aggiornare allora false</param>
        /// <returns>void</returns>
        public async Task SaveFilmAsync(Film item, bool isNew = false)
        {
            var uri = new Uri(String.Format(App.FILM_URL, string.Empty));

            //converto gli attributi di "item" in un file json
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                if (isNew)
                    response = await client.PostAsync(uri, content);
                else
                    response = await client.PutAsync(uri, content);


                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				TodoItem successfully saved.");
            } catch (Exception e) {
                Debug.WriteLine(@"				ERROR{0}", e);
            }            
        }
    }
}

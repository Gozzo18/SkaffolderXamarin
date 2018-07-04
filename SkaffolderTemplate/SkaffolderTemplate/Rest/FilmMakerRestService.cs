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
    public class FilmMakerRestService
    {
        HttpClient client;
        List<FilmMaker> filmMakers { get; set; }


        public FilmMakerRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        //DELETE
        /// <summary>
        /// Cancella un film maker
        /// </summary>
        /// <param name="id">Id del film maker da cancellare</param>
        /// <returns>void</returns>
        public async Task DeleteFilmMakerAsync(string id)
        {
            var uri = new Uri(String.Format(App.FILMMAKER_URL + id, string.Empty));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Film successfully deleted.");
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Ottieni la lista di film maker memorizzati
        /// </summary>
        /// <returns>Lista di attori</returns>
        public async Task<List<FilmMaker>> RefreshDataAsync()
        {
            filmMakers = new List<FilmMaker>();

            var uri = new Uri(String.Format(App.FILMMAKER_URL, string.Empty));

            try
            {
                //var content = await response.Content.ReadAsStringAsync();
                var content = await client.GetStringAsync(uri);
                filmMakers = JsonConvert.DeserializeObject<List<FilmMaker>>(content);

            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return filmMakers;
        }

        //POST
        /// <summary>
        /// Inserisce/Modifica i dati di un film maker
        /// </summary>
        /// <param name="item">Film maker da inserire o aggiornare</param>
        /// <param name="isNew">Inserire allora true, Aggiornare allora false</param>
        /// <returns>void</returns>
        public async Task SaveFilmMakerAsync(FilmMaker item, bool isNew = false)
        {
            var uri = new Uri(String.Format(App.FILMMAKER_URL, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                if (!isNew)
                    response = await client.PostAsync(uri, content);
                else
                    response = await client.PutAsync(uri, content);


                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				FilmMaker successfully saved.");
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}

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
    public class ActorRestService
    {
        HttpClient client;
        List<FilmMaker> attori { get; set; }


        public ActorRestService()
        {
            client = new HttpClient();
        }

        //DELETE
        /// <summary>
        /// Cancella un attore
        /// </summary>
        /// <param name="id">Id dell'attore da cancellare</param>
        /// <returns>void</returns>
        public async Task DeleteActorAsync(string id)
        {
            var uri = new Uri(String.Format(App.ACTOR_URL + id, string.Empty));

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
        /// Ottieni la lista di attori memorizzati
        /// </summary>
        /// <returns>Lista di attori</returns>
        public async Task<List<FilmMaker>> RefreshDataAsync()
        {
            attori = new List<FilmMaker>();

            var uri = new Uri(String.Format(App.ACTOR_URL, string.Empty));

            try
            {
                //var content = await response.Content.ReadAsStringAsync();
                var content = await client.GetStringAsync(uri);
                attori = JsonConvert.DeserializeObject<List<FilmMaker>>(content);

            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return attori;
        }

        //POST
        /// <summary>
        /// Inserisce/Modifica i dati di un attore
        /// </summary>
        /// <param name="item">Attore da inserire o aggiornare</param>
        /// <param name="isNew">Inserire allora true, Aggiornare allora false</param>
        /// <returns>void</returns>
        public async Task SaveActorAsync(FilmMaker item, bool isNew = false)
        {
            var uri = new Uri(String.Format(App.ACTOR_URL, string.Empty));

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
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}

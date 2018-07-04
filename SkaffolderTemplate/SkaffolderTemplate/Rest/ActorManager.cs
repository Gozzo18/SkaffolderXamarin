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
    public class ActorManager
    {
        HttpClient client;
        List<Actor> attori { get; set; }

        public ActorManager()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        //DELETE
        /// <summary>
        /// Cancella un attore
        /// </summary>
        /// <param name="id">Id dell'attore da cancellare</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(App.ACTOR_URL + id);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Actor successfully deleted.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //POST
        /// <summary>
        /// Inserisce un attore
        /// </summary>
        /// <param name="item">Attore da inserire</param>
        /// <returns>void</returns>
        public async Task POST(Actor item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                response = await client.PostAsync(App.ACTOR_URL, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Actor successfully saved.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Modifica un attore già presente
        /// </summary>
        /// <param name="item">Attore da modificare</param>
        /// <returns></returns>
        public async Task PUT(Actor item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.ACTOR_URL + item._id, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Actor successfully saved.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Ottieni la lista di attori memorizzati
        /// </summary>
        /// <returns>Lista di attori</returns>
        public async Task<List<Actor>> GET()
        {
            attori = new List<Actor>();
            var uri = new Uri(String.Format(App.ACTOR_URL, string.Empty));

            try
            {
                var content = await client.GetStringAsync(uri);
                attori = JsonConvert.DeserializeObject<List<Actor>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return attori;
        }
    }
}

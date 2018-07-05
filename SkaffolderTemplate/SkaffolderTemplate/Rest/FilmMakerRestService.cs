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
        public async Task DELETE(string id)
        {
          

            try
            {
                var response = await client.DeleteAsync(App.FILMMAKER_URL + id);

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
        public async Task<List<FilmMaker>> GETList()
        {
            filmMakers = new List<FilmMaker>();

            try
            {
                var content = await client.GetStringAsync(App.FILMMAKER_URL);
                filmMakers = JsonConvert.DeserializeObject<List<FilmMaker>>(content);

            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return filmMakers;
        }

        //POST
        /// <summary>
        /// Inserisce i dati di un film-maker
        /// </summary>
        /// <param name="item">Film maker da inserire</param>
        /// <returns>void</returns>
        public async Task SaveFilmMakerAsync(FilmMaker item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(App.FILMMAKER_URL,content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				FilmMaker successfully saved.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Modifica un film-maker già presente
        /// </summary>
        /// <param name="item">Film-Maker da modificare</param>
        /// <returns></returns>
        public async Task PUT(FilmMaker item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(App.FILMMAKER_URL + item._id, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"				Film-Maker successfully saved.");
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}

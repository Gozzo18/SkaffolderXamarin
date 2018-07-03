using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkaffolderTemplate.Rest
{
    public class FilmManager
    {
        FilmRestService restService; 

        public FilmManager(FilmRestService service)
        {
            restService = service;
        }

        /// <summary>
        /// Richiesta GET per i film
        /// </summary>
        /// <returns>Una lista di film</returns>
        public Task<List<Film>> GET()
        {
            return restService.RefreshDataAsync();
        }

        public Task POST(Film item, bool isNew=false)
        {
            return restService.SaveFilmAsync(item, isNew);
        }

        /// <summary>
        /// Richiesta DELETE per un film
        /// </summary>
        /// <param name="item">Film da eliminare</param>
        /// <returns></returns>
        public Task DELETE(Film item)
        {
            return restService.DeleteFilmAsync(item._id);
        }
    }
}

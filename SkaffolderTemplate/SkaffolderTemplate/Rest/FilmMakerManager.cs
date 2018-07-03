using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkaffolderTemplate.Rest
{
    public class FilmMakerManager
    {
        FilmMakerRestService service;

        public FilmMakerManager(FilmMakerRestService s)
        {
            service = s;
        }

        /// <summary>
        /// Richiesta GET per i film maker
        /// </summary>
        /// <returns>Una lista di film</returns>
        public Task<List<FilmMaker>> GET()
        {
            return service.RefreshDataAsync();
        }

        public Task POST(FilmMaker item, bool isNew = false)
        {
            return service.SaveFilmMakerAsync(item, isNew);
        }

        public Task DELETE(FilmMaker item)
        {
            return service.DeleteFilmMakerAsync(item.ID);
        }
    }
}

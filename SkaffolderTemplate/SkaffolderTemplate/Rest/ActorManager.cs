using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkaffolderTemplate.Rest
{
    public class ActorManager
    {
        ActorRestService service;

        public ActorManager(ActorRestService s)
        {
            service = s;
        }


        /// <summary>
        /// Richiesta GET per gli attori
        /// </summary>
        /// <returns>Una lista di attori</returns>
        public Task<List<FilmMaker>> GET()
        {
            return service.RefreshDataAsync();
        }

        public Task POST(FilmMaker item, bool isNew = false)
        {
            return service.SaveActorAsync(item, isNew);
        }

        public Task DELETE(FilmMaker item)
        {
            return service.DeleteActorAsync(item.ID);
        }
    }
}

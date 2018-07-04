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
        public Task<List<Actor>> GET()
        {
            return service.RefreshDataAsync();
        }

        /// <summary>
        /// Richiesta POST per gli attori
        /// </summary>
        /// <param name="item">Attore da aggiungere</param>
        /// <returns></returns>
        public Task POST(Actor item)
        {
            return service.SaveActorAsync(item);
        }

        /// <summary>
        /// Richiesta PUT per gli attori
        /// </summary>
        /// <param name="item">Attore da modificare</param>
        /// <returns></returns>
        public Task PUT(Actor item)
        {
            return service.UpdateActorAsync(item);
        }

        /// <summary>
        /// Richiesta DELETE per gli attori
        /// </summary>
        /// <param name="item">Film da eleminare</param>
        /// <returns></returns>
        public Task DELETE(Actor item)
        {
            return service.DeleteActorAsync(item._id);
        }
    }
}

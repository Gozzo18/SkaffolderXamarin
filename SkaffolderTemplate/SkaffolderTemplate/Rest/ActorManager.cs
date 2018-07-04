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
        /// <param name="item">Attore da aggiungere o aggiornare</param>
        /// <param name="isNew">True = aggiungere, False = aggiornare</param>
        /// <returns></returns>
        public Task POST(Actor item, bool isNew = false)
        {
            return service.SaveActorAsync(item, isNew);
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

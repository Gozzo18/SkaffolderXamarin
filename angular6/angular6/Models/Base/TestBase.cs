using System;
using Newtonsoft.Json;
using angular6.ViewModels;

namespace angular6.Models.Base
{
    public class TestBase : BaseViewModel
    {
        // Id Start 
		private string _id;
		[JsonProperty(PropertyName = "_id")]
		public string Id
		{
			get
			{
				return _id;
			}
			set
				{
					SetValue(ref _id, value);
				}
			}
		  // Id End 

        
        private string nome;
        [JsonProperty(PropertyName = "nome")]
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                SetValue(ref nome, value);
            }
        }
        
        
    }
}
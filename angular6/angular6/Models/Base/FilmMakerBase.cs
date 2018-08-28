using System;
using Newtonsoft.Json;
using angular6.ViewModels;

namespace angular6.Models.Base
{
    public class FilmMakerBase : BaseViewModel
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

        
        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetValue(ref name, value);
            }
        }
        private string surname;
        [JsonProperty(PropertyName = "surname")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                SetValue(ref surname, value);
            }
        }
        
        
    }
}
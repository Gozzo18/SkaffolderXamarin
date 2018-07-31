using Newtonsoft.Json;
using SkaffolderTemplate.ViewModels;
using System;

namespace SkaffolderTemplate.Models.Base
{
    public class ActorBase : BaseViewModel
    {
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

        private DateTime birthDate;
        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate
        {
            get
            {
                //If not specified, birthDate would be set as UTC time
                return birthDate.ToLocalTime();
            }
            set
            {
                SetValue(ref birthDate, value);
            }
        }

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

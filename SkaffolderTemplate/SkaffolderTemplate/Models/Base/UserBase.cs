﻿using Newtonsoft.Json;
using SkaffolderTemplate.ViewModels;

namespace SkaffolderTemplate.Models.Base
{
    public class UserBase : BaseViewModel
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

        private string username;
        [JsonProperty(PropertyName = "username")]
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                SetValue(ref username, value);
            }
        }

        private string password;
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetValue(ref password, value);
            }
        }

        private string[] roles;
        [JsonProperty(PropertyName = "roles")]
        public string[] Roles
        {
            get
            {
                return roles;
            }
            set
            {
                SetValue(ref roles, value);
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

        private string mail;
        [JsonProperty(PropertyName = "mail")]
        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                SetValue(ref mail, value);
            }
        }

        private string token;
        [JsonProperty(PropertyName = "token")]
        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                SetValue(ref token, value);
            }
        }
    }
}

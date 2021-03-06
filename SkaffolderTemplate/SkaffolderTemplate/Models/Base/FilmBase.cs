﻿using Newtonsoft.Json;
using SkaffolderTemplate.ViewModels;

namespace SkaffolderTemplate.Models.Base
{
     public class FilmBase : BaseViewModel
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

        private string genre;
        [JsonProperty(PropertyName = "genre")]
        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                SetValue(ref genre, value);
            }
        }

        private string title;
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetValue(ref title, value);
            }
        }

        private int year;
        [JsonProperty(PropertyName = "year")]
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                SetValue(ref year, value);
            }
        }

        private string filmMaker;
        [JsonProperty(PropertyName = "filmMaker")]
        public string FilmMaker
        {
            get
            {
                return filmMaker;
            }
            set
            {
                SetValue(ref filmMaker, value);
            }
        }

        private string[] cast;
        [JsonProperty(PropertyName = "cast")]
        public string[] Cast
        {
            get
            {
                return cast;
            }
            set
            {
                SetValue(ref cast, value);
            }
        }
    }
}

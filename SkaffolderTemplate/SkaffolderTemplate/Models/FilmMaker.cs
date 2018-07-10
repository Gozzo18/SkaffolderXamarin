using SkaffolderTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkaffolderTemplate.Models
{
    public class FilmMaker : BaseViewModel
    {

        public string _id;
        public string ID
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

        public string name;
        public string NAME
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

        public string surname;
        public string SURNAME
        {
            get
            {
                return surname;
            }
            set
            {
                SetValue(ref surname, name);
            }
        }
    }
}

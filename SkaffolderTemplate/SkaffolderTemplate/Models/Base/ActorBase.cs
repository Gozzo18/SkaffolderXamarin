using SkaffolderTemplate.ViewModels;
using System;

namespace SkaffolderTemplate.Models.Base
{
    public class ActorBase : BaseViewModel
    {
        public string _id;
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

        public DateTime birthDate;
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                SetValue(ref birthDate, value);
            }
        }

        public string name;
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

        public string surname;
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

using SkaffolderTemplate.ViewModels;

namespace SkaffolderTemplate.Models
{
    public class FilmMakerBase : BaseViewModel
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
                SetValue(ref surname, name);
            }
        }
    }
}

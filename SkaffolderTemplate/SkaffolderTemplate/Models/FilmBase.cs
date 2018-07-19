using SkaffolderTemplate.ViewModels;

namespace SkaffolderTemplate.Models
{
     public class FilmBase : BaseViewModel
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

        public string genre;
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

        public string title;
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

        public int year;
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

        public string filmMaker;
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

        public string[] cast;
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

using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewsForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class FilmPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<Film> _filmsList;
        public ObservableCollection<Film> FilmsList
        {
            get
            {
                return _filmsList;
            }
            set
            {
                SetValue(ref _filmsList, value);
            }
        }

        private ObservableCollection<Film> _supportList;
        public ObservableCollection<Film> SupportList
        {
            get
            {
                return _supportList;
            }
            set
            {
                SetValue(ref _supportList, value);
            }
        }

        private bool _refreshing;
        public bool Refreshing
        {
            get
            {
                return _refreshing;
            }
            set
            {
                SetValue(ref _refreshing, value);
            }
        }

        private string _searchedWord;
        public string SearchedWord
        {
            get
            {
                return _searchedWord;
            }
            set
            {
                SetValue(ref _searchedWord, value);
            }
        }
        #endregion

        #region Command
        public ICommand Add { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand LoadData { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public ICommand EditFilm
        {
            get
            {
                return new Command(async (e) =>
                {
                    var film = (e as Film);
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PushAsync(new FilmEdit(null), false);
                });

            }
        }

        public ICommand DeleteFilm
        {
            get
            {
                return new Command(async (e) =>
                {
                    var film = (e as Film);
                    await App.actorService.DELETE(film._id);
                    await RefreshList();
                });

            }
        }
        #endregion

        public FilmPageViewModel()
        {
            Add = new Command(async vm => await AddNewFilm());
            Refresh = new Command(async vm => await RefreshList());
            LoadData = new Command<ObservableCollection<Film>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private async Task GetRequest()
        {
            FilmsList = await App.filmService.GETList();
            SupportList = new ObservableCollection<Film>(FilmsList);
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            FilmsList = await App.filmService.GETList();
            SupportList = new ObservableCollection<Film>(FilmsList);
            Refreshing = false;
        }

        private async Task AddNewFilm()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PushAsync(new FilmEdit(null), false);
        }

        private void SearchWord()
        {
            if (SearchedWord.Length >= 1)
                SearchedWord = char.ToUpper(SearchedWord[0]) + SearchedWord.Substring(1);
            if (string.IsNullOrWhiteSpace(SearchedWord))
                SupportList = new ObservableCollection<Film>(FilmsList);
            else
            {
                var tempRecords = FilmsList.Where(c => c.title.Contains(SearchedWord));
                SupportList = new ObservableCollection<Film>(tempRecords);
            }
        }
    }
}

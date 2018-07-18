using SkaffolderTemplate.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using SkaffolderTemplate.ViewsForm;
using System.Threading.Tasks;
using System.Linq;

namespace SkaffolderTemplate.ViewModels
{
    public class FilmMakerPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<FilmMaker> _filmMakersList;
        public ObservableCollection<FilmMaker> FilmMakersList
        {
            get
            {
                return _filmMakersList;
            }
            set
            {
                SetValue(ref _filmMakersList, value);
            }
        }

        private ObservableCollection<FilmMaker> _supportList;
        public ObservableCollection<FilmMaker> SupportList
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

        #region Commands
        public ICommand Add { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand SelectedFilmMaker { get; private set; }
        public ICommand LoadData { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public ICommand EditFilmMaker
        {
            get
            {
                return new Command(async (e) =>
                {
                    var filmMaker = (e as FilmMaker);
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(filmMaker), false);
                });

            }
        }

        public ICommand DeleteFilmMaker
        {
            get
            {
                return new Command(async (e) =>
                {
                    var filmMaker = (e as FilmMaker);
                    await App.filmMakerService.DELETE(filmMaker._id);
                    await RefreshList();
                });

            }
        }
        #endregion

        public FilmMakerPageViewModel()
        {
            Add = new Command(async vm => await AddNewFilmMaker());
            Refresh = new Command(async vm => await RefreshList());
            LoadData = new Command<ObservableCollection<FilmMaker>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private void SearchWord()
        {
            if (SearchedWord.Length >= 1)
                SearchedWord = char.ToUpper(SearchedWord[0]) + SearchedWord.Substring(1);
            if (string.IsNullOrWhiteSpace(SearchedWord))
                SupportList = new ObservableCollection<FilmMaker>(FilmMakersList);
            else
            {
                var tempRecords = FilmMakersList.Where(c => c.Name.Contains(SearchedWord));
                SupportList = new ObservableCollection<FilmMaker>(tempRecords);
            }
        }

        private async Task GetRequest()
        {
            FilmMakersList = await App.filmMakerService.GETList();
            SupportList = new ObservableCollection<FilmMaker>(FilmMakersList);
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            FilmMakersList = await App.filmMakerService.GETList();
            SupportList = new ObservableCollection<FilmMaker>(FilmMakersList);
            Refreshing = false;
        }

        private async Task AddNewFilmMaker()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(null), false);
        }
    }
}

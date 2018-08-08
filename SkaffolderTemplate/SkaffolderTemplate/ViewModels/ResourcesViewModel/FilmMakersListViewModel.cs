using Rg.Plugins.Popup.Services;
using SkaffolderTemplate.Extensions;
using SkaffolderTemplate.Models;
using SkaffolderTemplate.Support;
using SkaffolderTemplate.Views;
using SkaffolderTemplate.Views.Loading;
using SkaffolderTemplate.Views.Edit;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels.ResourcesViewModel
{
    public class FilmMakersListViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<FilmMaker> _filmmakersList;
        //This collection main purpose is to store data from API request 
        public ObservableCollection<FilmMaker> FilmMakersList
        {
            get
            {
                return _filmmakersList;
            }
            set
            {
                SetValue(ref _filmmakersList, value);
            }
        }

        private ObservableCollection<FilmMaker> _supportList;
        //This one instead is the ItemSource of the ListView. This allows to modify without any exceptions, the elements of the ListView.
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

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetValue(ref _isBusy, value);
            }
        }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get
            {
                return _isLoaded;
            }
            set
            {
                SetValue(ref _isLoaded, value);
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public ICommand EditFilmMakerCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var filmmaker = (e as FilmMaker);
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerLoadingView(filmmaker), false);
                });

            }
        }

        public ICommand DeleteFilmMakerCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    //Pop Up allert appear
                    await PopupNavigation.Instance.PushAsync(new ConfirmDeletePopUp());
                    MessagingCenter.Subscribe<ConfirmDeletePopUp, bool>(this, Events.ConfirmDelete, async (arg1, arg2) =>
                    {
                        //If Save button is tapped
                        if (arg2)
                        {
                            var filmmaker = (e as FilmMaker);
                            await App.filmmakerService.DELETE(filmmaker.Id);
                            await RefreshList();
                        }
                    });
                });
            }
        }
        #endregion

        public FilmMakersListViewModel()
        {
            AddCommand = new Command(async vm => await AddNewFilmMaker());
            RefreshCommand = new Command(async vm => await RefreshList());
            LoadDataCommand = new Command<ObservableCollection<FilmMaker>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            FilmMakersList = await App.filmmakerService.GETList();
            SupportList = new ObservableCollection<FilmMaker>(FilmMakersList);
            Refreshing = false;
        }

        private async Task AddNewFilmMaker()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(null), false);
        }

        private async Task GetRequest()
        {
            //Set ActivityIndicator visible, hide the ListView
            IsBusy = true;
            IsLoaded = false;

            FilmMakersList = await App.filmmakerService.GETList();
            SupportList = new ObservableCollection<FilmMaker>(FilmMakersList);

            //Once ListView finished loading, we stop ActivityIndicator and set visible again the ListView
            IsBusy = false;
            IsLoaded = true;
        }

        private void SearchWord()
        {
            //Capitalize first letter of SearcheWord
            if (SearchedWord.Length >= 1)
                SearchedWord = char.ToUpper(SearchedWord[0]) + SearchedWord.Substring(1);

            if (string.IsNullOrWhiteSpace(SearchedWord))
                SupportList = new ObservableCollection<FilmMaker>(FilmMakersList);
            else
            {
                //The filtering of elements is based on the elemnts id. In case you wish to change, just overwrite c.Id with c.YourField
                var tempRecords = FilmMakersList.Where(c => c.Id.Contains(SearchedWord));
                SupportList = new ObservableCollection<FilmMaker>(tempRecords);
            }
        }
    }
}

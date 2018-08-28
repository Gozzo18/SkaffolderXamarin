using Rg.Plugins.Popup.Services;
using angular6.Extensions;
using angular6.Models;
using angular6.Support;
using angular6.Views;
using angular6.Views.Loading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace angular6.ViewModels.ResourcesViewModel
{
    public class ActorListViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<Actor> _actorsList;
        //This collection main purpose is to store data from API request 
        public ObservableCollection<Actor> ActorsList
        {
            get
            {
                return _actorsList;
            }
            set
            {
                SetValue(ref _actorsList, value);
            }
        }

        private ObservableCollection<Actor> _supportList;
        //This one instead is the ItemSource of the ListView. This allows to modify without any exceptions, the elements of the ListView.
        public ObservableCollection<Actor> SupportList
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

        public ICommand EditActorCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var actor = (e as Actor);
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PushAsync(new ActorEditLoadingView(actor), false);
                });

            }
        }

        public ICommand DeleteActorCommand
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
                            var actor = (e as Actor);
                            await App.actorService.DELETE(actor.Id);
                            await RefreshList();
                        }
                    });
                });
            }
        }
        #endregion

        public ActorListViewModel()
        {
            AddCommand = new Command(async vm => await AddNewActor());
            RefreshCommand = new Command(async vm => await RefreshList());
            LoadDataCommand = new Command<ObservableCollection<Actor>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            ActorsList = await App.actorService.GETList();
            SupportList = new ObservableCollection<Actor>(ActorsList);
            Refreshing = false;
        }

        private async Task AddNewActor()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            
            await masterDetailPage.Detail.Navigation.PushAsync(new ActorEditLoadingView(null), false);
                                              
        }

        private async Task GetRequest()
        {
            //Set ActivityIndicator visible, hide the ListView
            IsBusy = true;
            IsLoaded = false;

            ActorsList = await App.actorService.GETList();
            SupportList = new ObservableCollection<Actor>(ActorsList);

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
                SupportList = new ObservableCollection<Actor>(ActorsList);
            else
            {
                //The filtering of elements is based on the elemnts id. In case you wish to change, just overwrite c.Id with c.YourField
                var tempRecords = ActorsList.Where(c => c.Id.Contains(SearchedWord));
                SupportList = new ObservableCollection<Actor>(tempRecords);
            }
        }
    }
}

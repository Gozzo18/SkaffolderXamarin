using Rg.Plugins.Popup.Services;
using SkaffolderTemplate.Extensions;
using SkaffolderTemplate.Models;
using SkaffolderTemplate.Support;
using SkaffolderTemplate.ViewsForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class ActorPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<Actor> _actorList;
        public ObservableCollection<Actor> ActorsList
        {
            get
            {
                return _actorList;
            }
            set
            {
                SetValue(ref _actorList, value);
            }
        }

        private ObservableCollection<Actor> _supportList;
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
        public ICommand Add { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand LoadData { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public ICommand EditActor
        {
            get
            {
                return new Command(async (e) =>
                {
                    var actor = (e as Actor);
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PushAsync(new ActorEdit(actor), false);
                });
                
            }
        }

        public ICommand DeleteActor
        {
            get
            {
                return new Command(async (e) =>
                {
                    await PopupNavigation.Instance.PushAsync(new ConfirmDeletePopUp());
                    MessagingCenter.Subscribe<ConfirmDeletePopUp, bool>(this, Events.ConfirmDelete, async (arg1, arg2)  =>
                    {
                        if (arg2)
                        {
                            var actor = (e as Actor);
                            await App.actorService.DELETE(actor._id);
                            await RefreshList();
                        }
                    });
                });
            }
        }
        #endregion

        public ActorPageViewModel()
        {
            Add = new Command(async vm => await AddNewActor());
            Refresh = new Command(async vm => await RefreshList());
            LoadData = new Command<ObservableCollection<Actor>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private async Task RefreshList()
        {
            SearchedWord = "";
            Refreshing = true;
            ActorsList = await App.actorService.GETList();
            SupportList = new ObservableCollection<Actor>(ActorsList);
            Refreshing = false;
        }

        private async Task AddNewActor()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PushAsync(new ActorEdit(null), false);
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
            if (SearchedWord.Length >= 1)
                SearchedWord = char.ToUpper(SearchedWord[0]) + SearchedWord.Substring(1);
            if (string.IsNullOrWhiteSpace(SearchedWord))
                SupportList = new ObservableCollection<Actor>(ActorsList);
            else
            {
                var tempRecords = ActorsList.Where(c => c.name.Contains(SearchedWord));
                SupportList = new ObservableCollection<Actor>(tempRecords);
            }
        }
    }
}

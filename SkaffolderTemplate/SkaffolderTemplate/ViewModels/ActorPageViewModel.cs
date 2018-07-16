using SkaffolderTemplate.Models;
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
        #endregion

        private readonly IPageService _pageService;

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
                    await _pageService.PushAsync(new ActorEdit(actor), false);
                });
                
            }
        }

        public ICommand DeleteActor
        {
            get
            {
                return new Command(async (e) =>
                {
                    var actor = (e as Actor);
                    await App.actorService.DELETE(actor._id);
                    await RefreshList();
                });

            }
        }
        #endregion

        public ActorPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Add = new Command(async vm => await AddNewActor());
            Refresh = new Command(async vm => await RefreshList());
         // SelectedActor = new Command<Actor>(async vm => await SelectedItem(vm));
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
            await _pageService.PushAsync(new ActorEdit(null), false);
        }

        private async Task GetRequest()
        {
            ActorsList = await App.actorService.GETList();
            SupportList = new ObservableCollection<Actor>(ActorsList);
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

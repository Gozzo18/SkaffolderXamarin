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
    public class TestListViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<Test> _testsList;
        //This collection main purpose is to store data from API request 
        public ObservableCollection<Test> TestsList
        {
            get
            {
                return _testsList;
            }
            set
            {
                SetValue(ref _testsList, value);
            }
        }

        private ObservableCollection<Test> _supportList;
        //This one instead is the ItemSource of the ListView. This allows to modify without any exceptions, the elements of the ListView.
        public ObservableCollection<Test> SupportList
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

        public ICommand EditTestCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var test = (e as Test);
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PushAsync(new TestEditLoadingView(test), false);
                });

            }
        }

        public ICommand DeleteTestCommand
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
                            var test = (e as Test);
                            await App.testService.DELETE(test.Id);
                            await RefreshList();
                        }
                    });
                });
            }
        }
        #endregion

        public TestListViewModel()
        {
            AddCommand = new Command(async vm => await AddNewTest());
            RefreshCommand = new Command(async vm => await RefreshList());
            LoadDataCommand = new Command<ObservableCollection<Test>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            TestsList = await App.testService.GETList();
            SupportList = new ObservableCollection<Test>(TestsList);
            Refreshing = false;
        }

        private async Task AddNewTest()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            
            await masterDetailPage.Detail.Navigation.PushAsync(new TestEditLoadingView(null), false);
                                              
        }

        private async Task GetRequest()
        {
            //Set ActivityIndicator visible, hide the ListView
            IsBusy = true;
            IsLoaded = false;

            TestsList = await App.testService.GETList();
            SupportList = new ObservableCollection<Test>(TestsList);

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
                SupportList = new ObservableCollection<Test>(TestsList);
            else
            {
                //The filtering of elements is based on the elemnts id. In case you wish to change, just overwrite c.Id with c.YourField
                var tempRecords = TestsList.Where(c => c.Id.Contains(SearchedWord));
                SupportList = new ObservableCollection<Test>(tempRecords);
            }
        }
    }
}

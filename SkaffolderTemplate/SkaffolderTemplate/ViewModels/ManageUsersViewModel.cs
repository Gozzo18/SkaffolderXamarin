using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewsForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class ManageUsersViewModel : BaseViewModel
    {

        #region Attributes and Properties
        private ObservableCollection<User> _userList;
        public ObservableCollection<User> UsersList
        {
            get
            {
                return _userList;
            }
            set
            {
                SetValue(ref _userList, value);
            }
        }

        private ObservableCollection<User> _supportList;
        public ObservableCollection<User> SupportList
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

        public ICommand DeleteUser
        {
            get
            {
                return new Command(async (e) =>
                {
                    var user = (e as User);
                    if (!user.Id.Equals(Application.Current.Properties["UserId"]))
                    {
                        await App.userService.DELETE(user.Id);
                        await RefreshList();
                    }
                    else
                    {
                        //Aggiungere pop up di allerta
                        return;
                    }
                        
                });

            }
        }
        #endregion

        public ManageUsersViewModel()
        {
            Add = new Command(async vm => await AddNewUser());
            Refresh = new Command(async vm => await RefreshList());
            LoadData = new Command<ObservableCollection<Actor>>(async vm => await GetRequest());
            SearchCommand = new Command(SearchWord);
        }

        private async Task RefreshList()
        {
            SearchedWord = "";
            Refreshing = true;
            UsersList = await App.userService.GETList();
            SupportList = new ObservableCollection<User>(UsersList);
            Refreshing = false;
        }

        private async Task AddNewUser()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PushAsync(new RegisterNewUser(), false);
        }

        private async Task GetRequest()
        {
            //Set ActivityIndicator visible, hide the ListView
            IsBusy = true;
            IsLoaded = false;

            UsersList = await App.userService.GETList();
            SupportList = new ObservableCollection<User>(UsersList);

            //Once ListView finished loading, we stop ActivityIndicator and set visible again the ListView
            IsBusy = false;
            IsLoaded = true;
        }

        private void SearchWord()
        {
            if (SearchedWord.Length >= 1)
                SearchedWord = char.ToUpper(SearchedWord[0]) + SearchedWord.Substring(1);
            if (string.IsNullOrWhiteSpace(SearchedWord))
                SupportList = new ObservableCollection<User>(UsersList);
            else
            {
                var tempRecords = UsersList.Where(c => c.Surname.Contains(SearchedWord));
                SupportList = new ObservableCollection<User>(tempRecords);
            }
        }
    }
}

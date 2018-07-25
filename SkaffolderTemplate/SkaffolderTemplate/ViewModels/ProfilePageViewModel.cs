using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Attributes and Properties

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetValue(ref _name, value);
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                SetValue(ref _surname, value);
            }
        }

        private string _mail;
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                SetValue(ref _mail, value);
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                SetValue(ref _user, value);
            }
        }
        #endregion

        #region Commands
        public ICommand ChangePassword { get; private set; }
        public ICommand SaveData { get; private set; }
        public ICommand Back { get; private set; }
        #endregion

        public ProfilePageViewModel(User userLogged)
        {
            var app = Application.Current as App;
            User = new User();

            User.Username = userLogged.Username;
            User.Password = userLogged.Password;
            User.Id = userLogged.Id;
            User.Roles = userLogged.Roles;

            Name = userLogged.Name;
            Surname = userLogged.Surname;
            Mail = userLogged.Mail;

            SaveData = new Command(async vm => await SaveInfo());
            ChangePassword = new Command(async vm => await ModifyPassword());
            Back = new Command(GoBack);
        }

        private void GoBack()
        {
            var masterPage = App.Current.MainPage as MasterDetailPage;
            masterPage.Detail = new NavigationPage(new HomePage());
        }

        private async Task SaveInfo()
        {
            User.Name = Name;
            User.Surname = Surname;
            User.Mail = Mail;

            await App.userService.PUT(User);

            var masterPage = App.Current.MainPage as MasterDetailPage;
            masterPage.Detail = new NavigationPage(new HomePage());
        }

        private Task ModifyPassword()
        {
            throw new NotImplementedException();
        }
    }
}

using SkaffolderTemplate.Views;
using SkaffolderTemplate.ViewsForm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetValue(ref _username, value);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetValue(ref _password, value);
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                SetValue(ref _errorMessage, value);
            }
        }

        private bool _errorData = false;
        public bool ErrorData
        {
            get
            {
                return _errorData;
            }
            set
            {
                SetValue(ref _errorData, value);
            }
        }
        #endregion

        #region Commands
        public ICommand LoginClicked { get; private set; }
        public ICommand CheckToken { get; private set; }
        #endregion

        public LoginPageViewModel()
        {
            LoginClicked = new Command(async vm => await VerifyData());
            CheckToken = new Command(async vm => await StepOverLogin());
        }

        private async Task StepOverLogin()
        {
            var app = App.Current as App;

            if(await App.loginService.VerifyToken(app.AuthenticationToken))
            {
                App.Current.MainPage = new MasterPage();
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            }

        }

        private async Task VerifyData()
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Empty Username or Password";
                ErrorData = true;
                return;
            }

            if(await App.loginService.LoginAsync(Username, Password))
            {
                App.Current.MainPage = new MasterPage();
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            }
            else
            {
                ErrorMessage = "Incorrect Username or Password";
                ErrorData = true;
            }
        }
    }
}

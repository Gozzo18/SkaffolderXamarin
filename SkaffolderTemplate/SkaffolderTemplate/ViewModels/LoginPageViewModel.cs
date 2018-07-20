using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Text;
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

        public ICommand LoginClicked { get; private set; }

        public LoginPageViewModel()
        {
            LoginClicked = new Command(VerifyData);
        }

        private async void VerifyData(object obj)
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Empty Username or Password";
                ErrorData = true;
                return;
            }

            if (Username.Equals("admin") && Password.Equals("pass"))
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

using angular6.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace angular6.ViewModels
{
    public class RegisterNewUserViewModel : BaseViewModel
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                SetValue(ref _confirmPassword, value);
            }
        }

        private string _role;
        public string Role
        {
            get
            {
                return _role;
            }
            set
            {
                SetValue(ref _role, value);
            }
        }

        private bool _errorData;
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
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SelectedRoleCommand { get; private set; }
        #endregion

        public RegisterNewUserViewModel()
        {
            SaveCommand = new Command(async vm => await VerifyData());
            BackCommand = new Command(async vm => await GoBack());
            SelectedRoleCommand = new Command<Picker>(vm => RoleCompleted(vm));
        }

        private void RoleCompleted(Picker picker)
        {
            Role = (string)picker.SelectedItem;
        }

        private async Task VerifyData()
        {
            //Checkif any field is emtpy
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Mail) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Role)){
                ErrorData = true;
                ErrorMessage = "One or more fields are empty";
                return;
            }

            //Check if Password and ConfirmPassword are equals
            if (!Password.Equals(ConfirmPassword))
            {
                ErrorData = true;
                ErrorMessage = "Passwords inserted do not match";
            }
            else
            {
                User user = new User();
                user.Name = Name;
                user.Surname = Surname;
                user.Mail = Mail;
                user.Username = Username;
                user.Password = Password;
                string[] roleToAdd = new string[]{ Role };
                user.Roles = roleToAdd;
                
                await App.userService.POST(user);
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();
            }
                
                
        }

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
        }
    }
}

using SkaffolderTemplate.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
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
        public ICommand Back { get; private set; }
        public ICommand Save { get; private set; }
        public ICommand SelectedRole { get; private set; }
        #endregion

        public RegisterNewUserViewModel()
        {
            Save = new Command(async vm => await VerifyData());
            Back = new Command(async vm => await GoBack());
            SelectedRole = new Command<Picker>(vm => RoleCompleted(vm));
        }

        private void RoleCompleted(Picker picker)
        {
            Role = (string)picker.SelectedItem;
        }

        private async Task VerifyData()
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Mail) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Role)){
                ErrorData = true;
                ErrorMessage = "One or more fields are empty";
                return;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                ErrorData = true;
                ErrorMessage = "Password inserted do not matches";
            }else{
                User user = new User();
                user.Name = Name;
                user.Surname = Surname;
                user.Mail = Mail;
                user.Username = Username;
                user.Password = Password;
                string[] roleToAdd = new string []{ Role};
                user.Roles = roleToAdd;
                if (await App.userService.POST(user))
                {
                    var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                    await masterDetailPage.Detail.Navigation.PopAsync();
                }
                else
                {
                    ErrorData = true;
                    ErrorMessage = "There is already an User with that email";
                }
            }
                
                
        }

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
        }
    }
}

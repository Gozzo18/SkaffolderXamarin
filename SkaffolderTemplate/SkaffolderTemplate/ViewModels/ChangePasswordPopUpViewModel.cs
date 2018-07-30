using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class ChangePasswordPopUpViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private string _oldPassword;
        public string OldPassword
        {
            get
            {
                return _oldPassword;
            }
            set
            {
                SetValue(ref _oldPassword, value);
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                SetValue(ref _newPassword, value);
            }
        }

        private string _confirmedPassword;
        public string ConfirmedPassword
        {
            get
            {
                return _confirmedPassword;
            }
            set
            {
                SetValue(ref _confirmedPassword, value);
            }
        }

        private string _errorField;
        public string ErrorField
        {
            get
            {
                return _errorField;
            }
            set
            {
                SetValue(ref _errorField, value);
            }
        }
        #endregion

        #region Commands
        public ICommand Save { get; private set; }
        #endregion

        public ChangePasswordPopUpViewModel()
        {
            Save = new Command(async vm => await SavePassword());
        }

        private async Task SavePassword()
        {
            if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmedPassword))
                ErrorField = "One or more fields are empty";
            else if (!OldPassword.Equals(Application.Current.Properties["Password"]))
                ErrorField = "Old password is not correct";
            else if (!NewPassword.Equals(ConfirmedPassword))
                ErrorField = "Old and new password do not match";
            else if (NewPassword.Equals(ConfirmedPassword))
            {
                if (await App.loginService.ChangePassword(OldPassword, NewPassword))
                    ErrorField = "Password successfully changed";
            }
           
        }
    }
}

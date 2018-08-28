using angular6.Models;
using angular6.Support;
using angular6.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace angular6.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        private bool _isAllowed;
        public bool IsAllowed
        {
            get
            {
                return _isAllowed;
            }
            set
            {
                SetValue(ref _isAllowed, value);
            }
        }

        #region Commands
        public ICommand GetUserById { get; private set; }
        public ICommand SetDetailPage { get; private set; }
        #endregion

        public MasterPageViewModel()
        {
            GetUserById = new Command(async vm => await GetIdRequest());
            SetDetailPage = new Command<Button>(vm => UpdateDetailPage(vm));

            if (!Settings.CurrentUserRole.Equals("ADMIN"))
                IsAllowed = false;
            else
                IsAllowed = true;
        }

        private async Task GetIdRequest()
        {
            User user = await App.userService.GETId((string)Settings.UserId);
            var masterPage = App.Current.MainPage as MasterDetailPage;
            masterPage.Detail = new NavigationPage(new Profile(user));
        }

        private void UpdateDetailPage(Button button)
        {
            MessagingCenter.Send<MasterPageViewModel, string>(this, Events.DetailPageChanged, button.Text);
        }
    }
}

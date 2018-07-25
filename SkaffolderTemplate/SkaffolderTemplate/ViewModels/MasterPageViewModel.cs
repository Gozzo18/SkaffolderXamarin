using SkaffolderTemplate.Models;
using SkaffolderTemplate.Support;
using SkaffolderTemplate.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {

        public ICommand GetUserById { get; private set; }
        public ICommand SetDetailPage { get; private set; }

        public MasterPageViewModel()
        {
            GetUserById = new Command(async vm => await GetIdRequest());
            SetDetailPage = new Command<Button>(vm => UpdateDetailPage(vm));
        }

        private async Task GetIdRequest()
        {
            User user = await App.userService.GETId((string)Application.Current.Properties["UserId"]);
            var masterPage = App.Current.MainPage as MasterDetailPage;
            masterPage.Detail = new NavigationPage(new Profile(user));
        }

        private void UpdateDetailPage(Button button)
        {
            MessagingCenter.Send<MasterPageViewModel, string>(this, Events.DetailPageChanged, button.Text);
        }

    }
}

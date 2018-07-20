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

        public ICommand LoginClicked { get; private set; }

        public LoginPageViewModel()
        {
            LoginClicked = new Command(VerifyData);
        }

        private async void VerifyData(object obj)
        {
            App.Current.MainPage = new MasterPage();
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PushAsync(new HomePage(), false);
        }
    }
}

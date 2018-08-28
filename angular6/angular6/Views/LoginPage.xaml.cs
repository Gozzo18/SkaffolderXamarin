using angular6.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        //Set ViewModel for BindingContext
        private LoginPageViewModel ViewModel
        {
            get
            {
                return BindingContext as LoginPageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public LoginPage ()
		{
            //Setting BindingContext
            ViewModel = new LoginPageViewModel();
			InitializeComponent ();
		}

        private void Login_Clicked(object sender, EventArgs e)
        {
            ViewModel.LoginClicked.Execute(null);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            Password.Focus();
        }
    }
}
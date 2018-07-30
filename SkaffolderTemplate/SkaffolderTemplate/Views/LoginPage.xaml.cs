using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
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
            ViewModel = new LoginPageViewModel();
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            //Check if token is present
            ViewModel.CheckToken.Execute(null);

            base.OnAppearing();
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
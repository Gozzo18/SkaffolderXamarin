using SkaffolderTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            Password.Focus();
        }

        private void Password_Completed(object sender, EventArgs e)
        {
            Login.Clicked +=Login_Clicked;
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            ViewModel.LoginClicked.Execute(null);
        }
    }
}
using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterNewUser : ContentPage
	{
        private RegisterNewUserViewModel ViewModel
        {
            get
            {
                return BindingContext as RegisterNewUserViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public RegisterNewUser ()
		{
            ViewModel = new RegisterNewUserViewModel();
			InitializeComponent ();
		}

        private void RoleSelected(object sender, EventArgs e)
        {
            ViewModel.SelectedRole.Execute(sender as Picker);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            Picker.Focus();
        }
    }
}
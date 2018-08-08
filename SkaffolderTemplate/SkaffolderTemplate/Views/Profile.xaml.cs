using Rg.Plugins.Popup.Services;
using SkaffolderTemplate.Extensions;
using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Profile : ContentPage
	{
        private ProfilePageViewModel ViewModel
        {
            get
            {
                return BindingContext as ProfilePageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public Profile (User user)
		{
            BindingContext = new ProfilePageViewModel(user);
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ChangePasswordPopUp());
        }
    }
}
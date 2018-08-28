using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace angular6.Extensions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TokenExpiredPopUp : PopupPage
	{
		public TokenExpiredPopUp()
		{
			InitializeComponent();
		}

        private void Ok(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
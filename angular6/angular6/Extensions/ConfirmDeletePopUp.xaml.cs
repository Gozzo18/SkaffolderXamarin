using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using angular6.Support;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Extensions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmDeletePopUp : PopupPage
	{
        public ConfirmDeletePopUp()
		{
			InitializeComponent();
		}

        private void Back(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void Confirm(object sender, EventArgs e)
        {
            //Send a message of confirmation, to whoever is subscribed to this PopUpPage
            MessagingCenter.Send<ConfirmDeletePopUp, bool>(this, Events.ConfirmDelete, true);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
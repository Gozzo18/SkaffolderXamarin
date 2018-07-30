using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkaffolderTemplate.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Extensions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmDeletePopUp : PopupPage
	{

        public ConfirmDeletePopUp ()
		{
			InitializeComponent ();
		}

        private void Back(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void Confirm(object sender, EventArgs e)
        {
            MessagingCenter.Send<ConfirmDeletePopUp, bool>(this, Events.ConfirmDelete, true);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
	public partial class NotAllowedPopUp : PopupPage
	{
		public NotAllowedPopUp ()
		{
			InitializeComponent ();
		}

        private void Cancel(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}
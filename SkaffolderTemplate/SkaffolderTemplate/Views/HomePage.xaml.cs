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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var label = sender as Label;
            var masterPage = App.Current.MainPage as MasterDetailPage;
            if (label.Text.Equals("Actor"))
                masterPage.Detail = new NavigationPage(new ActorPage());
            else if(label.Text.Equals("Film"))
                masterPage.Detail = new NavigationPage(new FilmPage());
            else if (label.Text.Equals("FilmMaker"))
                masterPage.Detail = new NavigationPage(new FilmMakerPage());
        }
    }
}
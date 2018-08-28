using angular6.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
             if (!Settings.CurrentUserRole.Equals("ADMIN"))
                UserLabel.IsVisible = false;
            else
                UserLabel.IsVisible = true;
		}

        /// <summary>
        /// Set the Detail page for MasterDetailPage 
        /// </summary>
        /// <param name="sender">Label pressed</param>
        /// <param name="e"></param>
        private void ChangePage(object sender, EventArgs e)
        {
            var label = sender as Label;
            var masterPage = App.Current.MainPage as MasterDetailPage;
            if (label.Text.Equals("Actor"))
                masterPage.Detail = new NavigationPage(new ActorList());
                
            if (label.Text.Equals("Film"))
                masterPage.Detail = new NavigationPage(new FilmList());
                
            if (label.Text.Equals("FilmMaker"))
                masterPage.Detail = new NavigationPage(new FilmMakerList());
                
            if (label.Text.Equals("User"))
                
                masterPage.Detail = new NavigationPage(new UsersListStatic());
        }
    }
}
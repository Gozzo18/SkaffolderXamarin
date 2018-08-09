using SkaffolderTemplate.Views;
using System;
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

        /// <summary>
        /// Set the Detail page for MasterDetailPage 
        /// </summary>
        /// <param name="sender">Label pressed</param>
        /// <param name="e"></param>
        private void ChangePage(object sender, EventArgs e)
        {
            var label = sender as Label;
            var masterPage = App.Current.MainPage as MasterDetailPage;
            if (label.Text.Equals("Actors"))
                masterPage.Detail = new NavigationPage(new ActorsList());
                
            if (label.Text.Equals("Films"))
                masterPage.Detail = new NavigationPage(new FilmsList());
                
            if (label.Text.Equals("FilmMakers"))
                masterPage.Detail = new NavigationPage(new FilmMakersList());
                
            if (label.Text.Equals("Users"))
                
                masterPage.Detail = new NavigationPage(new UsersListStatic());
        }
    }
}
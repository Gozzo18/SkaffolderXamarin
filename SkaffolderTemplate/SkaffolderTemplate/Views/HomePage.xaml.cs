using SkaffolderTemplate.Views.List;
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
            if (label.Text.Equals("Actor"))
                masterPage.Detail = new NavigationPage(new ActorsList());
            if (label.Text.Equals("Film"))
                masterPage.Detail = new NavigationPage(new FilmsList());
            if (label.Text.Equals("FilmMaker"))
                masterPage.Detail = new NavigationPage(new FilmMakersList());
            if (label.Text.Equals("User"))
                masterPage.Detail = new NavigationPage(new UsersList());
        }
    }
}
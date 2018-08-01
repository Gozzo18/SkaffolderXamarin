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

        protected override void OnAppearing()
        {
            //Force garbace collector to run
            GC.Collect();
            GC.WaitForPendingFinalizers();
            base.OnAppearing();
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
                masterPage.Detail = new NavigationPage(new ActorPage());
            else if (label.Text.Equals("Film"))
                masterPage.Detail = new NavigationPage(new FilmPage());
            else if (label.Text.Equals("FilmMaker"))
                masterPage.Detail = new NavigationPage(new FilmMakerPage());
        }
    }
}
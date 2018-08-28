using angular6.Models;
using angular6.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeLoadingView : ContentPage
	{
        private  ;
        

		public HomeLoadingView ( ToEdit)
		{
             = ToEdit;
            
			InitializeComponent();
		}

        //Load data that will be used by Edit
        protected override async void OnAppearing()
        {
            if ( != null)
            {
                
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new Edit(), false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new Edit(null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
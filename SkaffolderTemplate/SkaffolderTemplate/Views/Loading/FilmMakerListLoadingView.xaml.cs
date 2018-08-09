using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerListLoadingView : ContentPage
	{
        private FilmMaker filmmaker;
        

		public FilmMakerListLoadingView (FilmMaker filmmakerToEdit)
		{
            filmmaker = filmmakerToEdit;
            
			InitializeComponent();
		}

        //Load data that will be used by FilmMakerEdit
        protected override async void OnAppearing()
        {
            if (filmmaker != null)
            {
                
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(filmmaker), false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
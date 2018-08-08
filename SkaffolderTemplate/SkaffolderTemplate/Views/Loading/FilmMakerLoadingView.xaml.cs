using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views.Edit;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerLoadingView : ContentPage
	{
        private FilmMaker filmmaker;
        

		public LoadingView (FilmMaker filmmakerToEdit)
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
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(filmmaker, false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmMakerEdit(null, null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
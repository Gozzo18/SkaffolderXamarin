using angular6.Models;
using angular6.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmListLoadingView : ContentPage
	{
        private Film film;
        
        private ObservableCollection<Actor> actors;
        

		public FilmListLoadingView (Film filmToEdit)
		{
            film = filmToEdit;
            
            actors = new ObservableCollection<Actor>();
            
			InitializeComponent();
		}

        //Load data that will be used by FilmEdit
        protected override async void OnAppearing()
        {
            if (film != null)
            {
                
                foreach (string actorId in film.Cast)
                {
                     actors.Add(await App. actorService.GETId(actorId));
                }
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmEdit(film,  actors), false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmEdit(null, null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
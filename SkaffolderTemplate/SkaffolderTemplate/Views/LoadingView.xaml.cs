using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views.Edit;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingView : ContentPage
	{
        private Film film;
        private ObservableCollection<Actor> actors;

		public LoadingView (Film filmToEdit)
		{
            film = filmToEdit;
            actors = new ObservableCollection<Actor>();
			InitializeComponent();
		}

        //Load data that will be used by FilmEdit
        protected override async void OnAppearing()
        {
                foreach (string actorId in film.Cast)
                {
                    actors.Add(await App.actorService.GETId(actorId));
                }
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new FilmEdit(film, actors), false);
        }
    }
}
using SkaffolderTemplate.Models;
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
	public partial class FilmMakerPage : ContentPage
	{
		public FilmMakerPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListaDiFilmMaker.ItemsSource = await App.filmMakerManager.GET();
        }

        //Ricarica la lista di film maker inseriti nel caso di aggiornamenti
        private async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            ListaDiFilmMaker.ItemsSource = await App.filmMakerManager.GET();

            list.IsRefreshing = false;
        }

        private async void eliminaFilmMaker(object sender, ItemTappedEventArgs e)
        {
            var selezionato = ((ListView)sender).SelectedItem;
            FilmMaker filmMakerDaEliminare = (FilmMaker)selezionato;

            var conferma = await DisplayAlert("Sei sicuro?", "Vuoi cancellare questo attore dalla lista?", "Conferma", "Indietro");

            if (conferma)
                await App.filmMakerManager.DELETE(filmMakerDaEliminare);

            OnRefresh(ListaDiFilmMaker, null);
        }
    }
}
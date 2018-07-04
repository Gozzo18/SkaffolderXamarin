using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmPage : ContentPage
	{
		public FilmPage ()
		{
			InitializeComponent ();
		}

        //Appena appare la view, carichiamo i film già presenti
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listaDiFilm.ItemsSource = await App.filmManager.GET();
        }

        //Ricarica la lista dei film inseriti nel caso di aggiornamenti
        private async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            listaDiFilm.ItemsSource = await App.filmManager.GET();

            list.IsRefreshing = false;
        }

        private async void paginaAttori(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActorPage(), false);
            return;
        }

        private async void paginaFilmMaker(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilmMakerPage(),false);
        }

        //Elimina il film selezionato
        private async void eliminaFilm(object sender, SelectedItemChangedEventArgs e)
        {
            var selezionato = ((ListView)sender).SelectedItem;
            Film filmDaEliminare = (Film)selezionato;
            Debug.WriteLine("L'id del film da eliminare è : " + filmDaEliminare._id);
            var conferma = await DisplayAlert("Sei sicuro?", "Vuoi cancellare questo film dalla lista?", "Conferma", "Indietro");

            if (conferma)
                await App.filmManager.DELETE(filmDaEliminare);

             OnRefresh(listaDiFilm,null);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewsForm;
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
            listaDiFilm.ItemsSource = await App.filmService.GETList();
        }

        //Ricarica la lista dei film inseriti nel caso di aggiornamenti
        private async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            listaDiFilm.ItemsSource = await App.filmService.GETList();

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

        private async void editFilm(object sender, SelectedItemChangedEventArgs e)
        {
            var selezionato = ((ListView)sender).SelectedItem;
            Film film = (Film)selezionato;

            //Cancella = TRUE, Modifica = false
            var scelta = await DisplayAlert("EDIT", "Vuoi cancellare o modificare questo film?", "Cancella", "Modifica");

            if (scelta)
            {
                await App.filmService.DELETE(film._id);
                OnRefresh(listaDiFilm, null);
            }
            else
            {
                await Navigation.PushAsync(new FilmEdit(film), false);
            }

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void aggiungiFilm(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilmEdit(null));
        }
    }
}
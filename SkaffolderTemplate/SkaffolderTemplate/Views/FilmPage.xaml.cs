using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModel;
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
            listaDiFilm.ItemsSource = await App.fm.GET();
        }

        //Ricarica la lista dei film inseriti nel caso di aggiornamenti
        private async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            listaDiFilm.ItemsSource = await App.fm.GET();

            list.IsRefreshing = false;
        }

      /*  private async void cancellaElemento(object sender, ItemTappedEventArgs e)
        {
            Film film = ((Film)((ListView)sender).SelectedItem);
            var conferma = await DisplayAlert("Sei sicuro?", "Vuoi cancellare questo film dalla lista?", "Conferma", "Indietro");

            if (conferma)
                 await App.fm.DELETE(film);
        }

        private async void aggiungiFilm(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(FormInserimentoFilm);
        }*/
    }
}
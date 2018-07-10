using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ObservableCollection<FilmMaker> listaFilmMakers = await App.filmMakerService.GETList();
            BindingContext = new FilmMakerPageViewModel(listaFilmMakers, new PageService());
        }

        //Ricarica la lista di film maker inseriti nel caso di aggiornamenti
        private void OnRefresh(object sender, EventArgs e)
        {
            (BindingContext as FilmMakerPageViewModel).RefreshList();
        }

        private async Task aggiungiNuovoFilmMaker(object sender, EventArgs e)
        {
            await (BindingContext as FilmMakerPageViewModel).AggiungiFilmMaker();
        }

        private async Task EditFilmMaker(object sender, SelectedItemChangedEventArgs e)
        {
            var scelta = await DisplayActionSheet("Cancellare o modificare questo film maker?", "Indietro", "Eliminare", "Modifica");
            await (BindingContext as FilmMakerPageViewModel).SelectedItem(e.SelectedItem as FilmMaker, scelta);
        }
    }
}
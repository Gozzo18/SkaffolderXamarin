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
    }
}
using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewsForm;
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
	public partial class ActorPage : ContentPage
	{
		public ActorPage ()
		{
			InitializeComponent ();
            
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ListaDiAttori.ItemsSource = await App.actorManager.GET();
        }

        //Ricarica la lista di attori inseriti nel caso di aggiornamenti
        private async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;

            ListaDiAttori.ItemsSource = await App.actorManager.GET();

            list.IsRefreshing = false;
        }

        private async void editAttore(object sender, ItemTappedEventArgs e)
        {
            var selezionato = ((ListView)sender).SelectedItem;
            Actor attore = (Actor)selezionato;

            //Cancella = TRUE, Modifica = false
            var scelta = await DisplayAlert("EDIT", "Vuoi cancellare o modificare questo attore ?", "Cancella", "Modifica");

            if (scelta)
            {
                await App.actorManager.DELETE(attore);
                OnRefresh(ListaDiAttori, null);
            }
            else
            {
                await Navigation.PushAsync(new formInserimentoAttore(attore), false);
                return;
            }
        }

        private async void aggiungiNuovoAttore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new formInserimentoAttore(null),false);
        }
    }
}
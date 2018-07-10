using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using SkaffolderTemplate.ViewsForm;
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
	public partial class ActorPage : ContentPage
	{
		public ActorPage ()
		{
			InitializeComponent ();            
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ObservableCollection<Actor> listaAttori = await App.actorService.GETList();
            BindingContext = new ActorPageViewModel(listaAttori, new PageService());
        }

        //Ricarica la lista di attori inseriti nel caso di aggiornamenti
        private void OnRefresh(object sender, EventArgs e)
        {
            (BindingContext as ActorPageViewModel).RefreshList();
        }

        private async Task aggiungiNuovoAttore(object sender, EventArgs e)
        {
           await (BindingContext as ActorPageViewModel).AggiungiAttore();
        }

        private async void EditAttore(object sender, SelectedItemChangedEventArgs e)
        {
            var scelta = await DisplayActionSheet("Cancellare o modificare questo film attore?", "Indietro", "Eliminare", "Modifica");
            await (BindingContext as ActorPageViewModel).SelectedItem(e.SelectedItem as Actor, scelta);
        }
    }
}
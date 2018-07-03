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
    }
}
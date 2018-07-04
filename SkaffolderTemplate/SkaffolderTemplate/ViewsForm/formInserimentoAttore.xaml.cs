using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class formInserimentoAttore : ContentPage
	{
		public formInserimentoAttore ()
		{
			InitializeComponent ();
           
		}

        private async void tornaIndietro(object sender, EventArgs e)
        {
            await Navigation.PopAsync(false);
        }

        private async void salvaAttore(object sender, EventArgs e)
        {
            Actor datiAttore = new Actor();
            datiAttore.name = nomeAttore.Text;
            datiAttore.surname = cognomeAttore.Text;
            datiAttore.birthDate = datapicker.Date;

            await App.actorManager.POST(datiAttore, false);

            await Navigation.PushAsync(new ActorPage(), false);
        }

        private void nomeAttore_Completed(object sender, EventArgs e)
        {
            cognomeAttore.Focus();
        }

        private void cognomeAttore_Completed(object sender, EventArgs e)
        {
            datapicker.Focus();
        }
    }
}
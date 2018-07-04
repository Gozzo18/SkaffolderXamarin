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

        //booleano per vedere se l'attore è da modificare o da aggiungere: Aggiungere = TRUE, Modificare = FALSE
        public bool isPresent = false;
        public string idAttore = null;

		public formInserimentoAttore (Actor attorePassato)
		{
			InitializeComponent ();

            //Se i dati dell'attore sono da modificare, passo la form già completata
            if (attorePassato != null)
            {
                datapicker.Date = attorePassato.birthDate;
                nomeAttore.Text = attorePassato.name;
                cognomeAttore.Text = attorePassato.surname;
                idAttore = attorePassato._id;
                isPresent = true;
            }
           
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
            datiAttore._id = idAttore;

            if (isPresent)
                await App.actorManager.PUT(datiAttore);
            else
                await App.actorManager.POST(datiAttore);

            await Navigation.PushAsync(new ActorPage(), false);
            return;
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
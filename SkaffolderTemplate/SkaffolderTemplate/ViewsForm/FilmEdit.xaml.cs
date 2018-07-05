using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmEdit : ContentPage
	{
        //booleano per vedere se il film è da modificare o da aggiungere: Aggiungere = TRUE, Modificare = FALSE
        public bool isPresent = false;

        public string idFilm = null;



        public FilmEdit (Film filmPassato)
		{
			InitializeComponent ();

            //Se i dati del film sono da modificare, passo la form già completata
            if (filmPassato != null)
            {
                titoloFilm.Text = filmPassato.title;
                annoFilm.Text = filmPassato.year.ToString();
                isPresent = true;
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            filmMakerPicker.ItemsSource = await App.filmMakerService.GETList();
            filmMakerPicker.ItemDisplayBinding = new Binding("surname");

            actorPicker.ItemsSource = await App.actorService.GETList();
            actorPicker.ItemDisplayBinding = new Binding("surname");

            var generi = new List<string>();
            generi.Add("Action");
            generi.Add("Crime");
            generi.Add("Fantasy");
            generi.Add("Horror");
            genereFilm.ItemsSource = generi;
      
        }

        private async void tornaIndietro(object sender, EventArgs e)
        {
            await Navigation.PopAsync(false);
        }

        private async void salvaFilm(object sender, EventArgs e)
        {
            Film datiFilm = new Film();
            salvaDatiFilm(datiFilm);

            if (isPresent)
                await App.filmService.PUT(datiFilm);
            else
                await App.filmService.POST(datiFilm);

            await Navigation.PushAsync(new FilmPage(), false);
            
        }

        private void salvaDatiFilm(Film datiFilm)
        {
            Debug.WriteLine("ID : " + datiFilm._id);
            datiFilm.title = titoloFilm.Text;
            Debug.WriteLine("Titolo : " + datiFilm.title);
            datiFilm.year = Int32.Parse(annoFilm.Text);
            Debug.WriteLine("Anno : " + datiFilm.year);
            datiFilm.genre = (string)genereFilm.SelectedItem;
            Debug.WriteLine("Genere : " + datiFilm.genre);
            datiFilm._id = idFilm;

            datiFilm.filmMaker = ((FilmMaker)filmMakerPicker.SelectedItem)._id;
            Debug.WriteLine("ID filmMaker : " + datiFilm.filmMaker);
        }

        private void titoloFilm_Completed(object sender, EventArgs e)
        {
            annoFilm.Focus();
        }

        private void annoFilm_Completed(object sender, EventArgs e)
        {
            genereFilm.Focus();
        }

        private async void aggiungiNuovoPicker(object sender, EventArgs e)
        {

            Picker picker = new Picker();
            picker.ItemsSource = await App.actorService.GETList();
            picker.ItemDisplayBinding = new Binding("surname");

            castDiAttori.Children.Add(picker);

        }
    }
}
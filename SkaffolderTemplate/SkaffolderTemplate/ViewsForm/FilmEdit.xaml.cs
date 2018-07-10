using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public List<string> idFilm;

        public ObservableCollection<FilmMaker> listaDiFilmakersDisponibili;
        public List<string> generiFilm;
        public ObservableCollection<Actor> listaDiAttoriDisponibili;

        public string genereFilmPassato { get; set; }
        public FilmMaker filmMakerPassato { get; set; } = new FilmMaker();
        public Actor attoreSingletonPassato { get; set; } = new Actor();
        public List<string> idAttoriPassati { get; set; } = new List<string>();
        public List<Actor> listaDiAttoriPassati { get; set; } = new List<Actor>();
        public string idFilmPassato { get; set; }
      

        public FilmEdit (Film filmPassato)
		{
            BindingContext = this;
			InitializeComponent ();
            idFilm = new List<string>();

            //Se i dati del film sono da modificare, passo la form già completata
            if (filmPassato != null)
            {
                idFilmPassato = filmPassato._id;
                titoloFilm.Text = filmPassato.title;
                annoFilm.Text = filmPassato.year.ToString();
                genereFilmPassato = filmPassato.genre;
                filmMakerPassato._id = filmPassato.filmMaker;
                for (int i = 0; i < filmPassato.cast.Length; i++)
                {
                    Debug.WriteLine("Id attori del filmPassato : " + filmPassato.cast[i]);
                    idAttoriPassati.Add(filmPassato.cast[i]);
                }
                isPresent = true;
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("E' Presente : " + isPresent);
            generiFilm = new List<string>();
            generiFilm.Add("Action");
            generiFilm.Add("Crime");
            generiFilm.Add("Fantasy");
            generiFilm.Add("Horror");
            inizializzaPickers();

        }


        private async void inizializzaPickers()
        {
            //Picker relativo ai filmMakers
            listaDiFilmakersDisponibili = await App.filmMakerService.GETList();
            pickerFilmMaker.ItemsSource = listaDiFilmakersDisponibili;
            pickerFilmMaker.ItemDisplayBinding = new Binding("surname");
                    
            //Picker relativo ai generi
            pickerGenereFilm.ItemsSource = generiFilm;
            
            //picker relativo agli attori
            listaDiAttoriDisponibili = await App.actorService.GETList();
            pickerCastAttori.ItemsSource = listaDiAttoriDisponibili;
            pickerCastAttori.ItemDisplayBinding = new Binding("surname");

            //se sto modificando il film, allora devo selezionare per i picker i valori già presenti
            if (isPresent)
            {
                filmMakerPassato = await App.filmMakerService.GETId(filmMakerPassato._id);
                pickerFilmMaker.SelectedIndex = pickerFilmMaker.Items.IndexOf(filmMakerPassato.surname);

                pickerGenereFilm.SelectedItem = genereFilmPassato;

                foreach(string s in idAttoriPassati)
                {
                    //prendo l'attore tramite il suo id
                    attoreSingletonPassato = await App.actorService.GETId(s);

                    //aggiungo la label con il suo cognome
                    attoriSelezionati.Children.Add(new Label
                    {
                        Text = attoreSingletonPassato.surname
                    });

                    //aggiungo l'id dell'attore alla lista idFilm che dovrà poi essere salvata
                    idFilm.Add(s);

                    //rimuovo l'attore dalla lista degli attori selezionabili
                    foreach(Actor a in listaDiAttoriDisponibili.ToList())
                    {
                        if (attoreSingletonPassato._id.Equals(a._id))
                        {
                            listaDiAttoriDisponibili.Remove(a);
                        }
                    }
                }

            }
        }

        private async void tornaIndietro(object sender, EventArgs e)
        {
            await Navigation.PopAsync(false);
        }

        private async void salvaFilm(object sender, EventArgs e)
        {
            Film filmDaSalvare = salvaDatiFilm();

            if (isPresent)
            {
                filmDaSalvare._id = idFilmPassato;
                await App.filmService.PUT(filmDaSalvare);
            }
            else
                await App.filmService.POST(filmDaSalvare);

            await Navigation.PushAsync(new FilmPage(), false);
            
        }


        /// <summary>
        /// Completato l'intero form, salvo i dati
        /// </summary>
        private Film salvaDatiFilm()
        {
            Film datiFilm = new Film();
            datiFilm.title = titoloFilm.Text;
            datiFilm.year = Int32.Parse(annoFilm.Text);
            datiFilm.genre = (string)pickerGenereFilm.SelectedItem;

            int i = 0;
            datiFilm.cast = new string[idFilm.Count];
            foreach (string s in idFilm)
            {
                datiFilm.cast[i] = s;
                i++;
            }

            datiFilm.filmMaker = ((FilmMaker)pickerFilmMaker.SelectedItem)._id;

            return datiFilm;           
        }

        /// <summary>
        /// Quando voglio aggiungere un nuovo attore mostro il pickerCastAttori
        /// </summary>
        private void aggiungiNuovoAttore(object sender, EventArgs e)
        {
            pickerCastAttori.Focus();
            pickerCastAttori.IsVisible = true;
        }

        /// <summary>
        /// Quando pickerCastAttori perde il focus, lo nascondo
        /// </summary>
        private void castDiAttori_Unfocused(object sender, FocusEventArgs e)
        {
            pickerCastAttori.IsVisible = false;
        }

        /// <summary>
        /// Selezionata una opzione dal pickerCastAttori, aggiungo allo stack attoriSelezionati una nuova label che mostra il cognome dell'attore
        /// Intolre memorizza nella lista idFilm l'id di quell'attore
        /// </summary>
        private  void attoreSelezionato(object sender, FocusEventArgs e)
        {
            if (pickerCastAttori.SelectedIndex != -1) { 
                Actor actorSelected = ((Actor)((Picker)sender).SelectedItem);

                idFilm.Add(actorSelected._id);
                Debug.WriteLine("ID dell'attore selezionato : " + actorSelected._id);
                attoriSelezionati.Children.Add(new Label
                {
                    Text = actorSelected.surname
                });

                listaDiAttoriDisponibili.Remove(actorSelected);

                /*Rimettendo l'index del picker a -1, viene invocato nuovamente questo metodo dunque, grazie alla verifica messa la seconda chiamata non produce nessuno effetto
                 *E' stato fatto questo per eliminare un determinato problema grafico che si verifica quando il picker riappare
                 */
                pickerCastAttori.SelectedIndex = -1;

            }
        }

        /// <summary>
        /// Completata l'entry relativa al titolo, sposto il focus sull'entry relativa all'anno
        /// </summary>
        private void titoloFilm_Completed(object sender, EventArgs e)
        {
            annoFilm.Focus();
        }

        /// <summary>
        /// Completata l'entry relativa all'anno, sposto il focus sul picker relativo ai generi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void annoFilm_Completed(object sender, EventArgs e)
        {
            pickerGenereFilm.Focus();
        }
    }
}
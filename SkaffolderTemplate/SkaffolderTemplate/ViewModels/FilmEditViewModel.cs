using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class FilmEditViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetValue(ref _id, value);
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetValue(ref _title, value);
            }
        }

        private string _year;
        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                SetValue(ref _year, value);
            }
        }

        private string _genre;
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                SetValue(ref _genre, value);
            }
        }

        private bool _isPresent;
        //True = editing Film, False = creating new Film
        public bool IsPresent
        {
            get
            {
                return _isPresent;
            }
            set
            {
                SetValue(ref _isPresent, value);
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                SetValue(ref _errorMessage, value);
            }
        }

        private FilmMaker _filmMaker;
        public FilmMaker FilmMaker
        {
            get
            {
                return _filmMaker;
            }
            set
            {
                SetValue(ref _filmMaker, value);
            }
        }

        private Film _film;
        public Film Film
        {
            get
            {
                return _film;
            }
            set
            {
                SetValue(ref _film, value);
            }
        }

        private ObservableCollection<Actor> _actorsCastInserted;
        //This is the collection of actors that are ALREADY inserted as cast members for the film
        public ObservableCollection<Actor> ActorsCastInserted
        {
            get
            {
                return _actorsCastInserted;
            }
            set
            {
                SetValue(ref _actorsCastInserted, value);
            }
        }

        private ObservableCollection<Actor> _actorsCastAvailable;
        //This is the collection of actors that CAN BE inserted as cast members for the film
        public ObservableCollection<Actor> ActorsCastAvailable
        {
            get
            {
                return _actorsCastAvailable;
            }
            set
            {
                SetValue(ref _actorsCastAvailable, value);
            }
        }

        private ObservableCollection<FilmMaker> _filmMakersAvailable;
        //This is the collection of filmMakers that can be inserted as filmMaker for the film
        public ObservableCollection<FilmMaker> FilmMakersAvailable
        {
            get
            {
                return _filmMakersAvailable;
            }
            set
            {
                SetValue(ref _filmMakersAvailable, value);
            }
        }
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand TitleCompletedCommand { get; private set; }
        public ICommand YearCompletedCommand { get; private set; }
        public ICommand SelectedGenreCommand { get; private set; }
        public ICommand SelectedActorCommand { get; private set; }
        public ICommand SelectedFilmMakerCommand { get; private set; }
        public ICommand SetPickersItemSourceCommand { get; private set; }
        public ICommand DeleteItemCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (e as Actor);
                    int i = 0 ;
                    bool found = false;
                    while (i < ActorsCastInserted.Count && !found)
                    {
                        if (item.Id.Equals(ActorsCastInserted[i].Id))
                        {
                            ActorsCastAvailable.Add(ActorsCastInserted[i]);
                            ActorsCastInserted.RemoveAt(ActorsCastInserted.IndexOf(ActorsCastInserted[i]));
                            found = true;
                        }
                        i++;
                    }
                });
            }
        }
        #endregion

        public FilmEditViewModel(Film filmToEdit, ObservableCollection<Actor> actors)
        {
            Film = filmToEdit;

            ActorsCastInserted = actors;

            SetPickersItemSourceCommand = new Command(async vm => await PopulatePickers());
            SaveCommand = new Command(async vm => await SaveFilmData());
            BackCommand = new Command(async vm => await GoBack());
            TitleCompletedCommand = new Command<Entry>(vm => TitleEntryCompleted(vm));
            YearCompletedCommand = new Command<Entry>(vm => YearEntryCompleted(vm));
            SelectedGenreCommand = new Command<Picker>(vm => GenreCompleted(vm));
            SelectedActorCommand = new Command<Picker>(vm => ActorCompleted(vm));
            SelectedFilmMakerCommand = new Command<Picker>(vm => FilmMakerCompleted(vm));
        }

        private async Task PopulatePickers()
        {
            ActorsCastAvailable = await App.actorService.GETList();
            FilmMakersAvailable = await App.filmMakerService.GETList();

            SetData();
        }

        private void SetData()
        {
            if (Film != null)
            {
                //Overwrite Title, Year and Genre entries
                Id = Film.Id;
                Title = Film.Title;
                Year = Film.Year.ToString();
                Genre = Film.Genre;

                //Overwrite FilmMaker entry
                for (int i = 0; i < FilmMakersAvailable.Count; i++)
                {
                    if (FilmMakersAvailable[i].Id.Equals(Film.FilmMaker))
                    {
                        FilmMaker = FilmMakersAvailable[i];
                    }
                }

                //Remove from ActorCastAvailable all the Actors which are already inserted
                for (int k = 0; k < ActorsCastAvailable.Count; k++)
                {
                    for (int h = 0; h < ActorsCastInserted.Count; h++)
                    {
                        if(ActorsCastAvailable.Count != 0)
                        {
                            if (ActorsCastInserted[h].Id.Equals(ActorsCastAvailable[k].Id))
                            {
                                ActorsCastAvailable.Remove(ActorsCastAvailable[k]);
                                k = 0;
                                h = 0;
                            }
                        }
                    }
                }
                IsPresent = true;
            }
            else
                Film = new Film();
        }

        private void TitleEntryCompleted(Entry FilmTitle)
        {
            Title = FilmTitle.Text;
        }

        private void YearEntryCompleted(Entry FilmYear)
        {
            Year = FilmYear.Text;
        }

        private void GenreCompleted(Picker picker)
        {
            Genre = (string)picker.SelectedItem;
        }

        private void ActorCompleted(Picker picker)
        {
           Actor actorSelected = (Actor)picker.SelectedItem;
            if (actorSelected != null)
            {
                ActorsCastInserted.Add(actorSelected);
                bool found = false;
                int iterator = 0;
                while(iterator < ActorsCastAvailable.Count && !found)
                {
                    if (actorSelected.Id.Equals(ActorsCastAvailable[iterator].Id))
                    {
                        found = true;
                    }
                    iterator++;
                }

                //DO NOT TOUCH
                //This allows to modify the ItemSource of the Picker dynamically where actors can be selected
                ObservableCollection<Actor> support = new ObservableCollection<Actor>(ActorsCastAvailable);
                support.RemoveAt(iterator-1);
                ActorsCastAvailable = support;
            }            
        }

        private void FilmMakerCompleted(Picker picker)
        {
            FilmMaker = (FilmMaker)picker.SelectedItem;
        }

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
           
        }

        private async Task SaveFilmData()
        {
            //Check if there is any empty field
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Genre) || string.IsNullOrWhiteSpace(Year) || FilmMaker == null)  
            {
                ErrorMessage = "One or more fields are empty";
                return;
            }
            else
            {
                Film.Title = Title;
                Film.Year = Int32.Parse(Year);
                Film.Genre = Genre;

                List<string> supportList = new List<string>();
                foreach (Actor a in ActorsCastInserted)
                    supportList.Add(a.Id);
                Film.Cast = supportList.ToArray();

                Film.FilmMaker = FilmMaker.Id;

                if (IsPresent)
                {
                    Film.Id = Film.Id;
                    await App.filmService.PUT(Film);
                }
                else
                    await App.filmService.POST(Film);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();
            }
            
        }
    }
}

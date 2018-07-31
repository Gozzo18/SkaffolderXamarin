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

        private ObservableCollection<string> _actorCastId = new ObservableCollection<string>();
        public ObservableCollection<string> ActorCastId
        {
            get
            {
                return _actorCastId;
            }
            set
            {
                SetValue(ref _actorCastId, value);
            }
        }

        private bool _isPresent;
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

        private Actor _actor;
        public Actor Actor
        {
            get
            {
                return _actor;
            }
            set
            {
                SetValue(ref _actor, value);
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
        public ICommand SetPickersItemSource { get; private set; }

        public ICommand TitleCompleted { get; private set; }
        public ICommand YearCompleted { get; private set; }
        public ICommand SelectedGenre { get; private set; }
        public ICommand SelectedActor { get; private set; }
        public ICommand SelectedFilmMaker { get; private set; }

        public ICommand DeleteItem
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
                            ActorCastId.Remove(item.Id);
                            ActorsCastInserted.RemoveAt(ActorsCastInserted.IndexOf(ActorsCastInserted[i]));
                            found = true;
                        }
                        i++;
                    }
                });
            }
        }

        public ICommand Back { get; private set; }
        public ICommand Save { get; private set; }

        #endregion

        public FilmEditViewModel(Film alreadyPresentFilm)
        {
            Film = alreadyPresentFilm;

            SetPickersItemSource = new Command(async vm => await PopulatePickers());
            Save = new Command(async vm => await SaveFilmData());
            Back = new Command(async vm => await GoBack());
            TitleCompleted = new Command<Entry>(vm => TitleEntryCompleted(vm));
            YearCompleted = new Command<Entry>(vm => YearEntryCompleted(vm));
            SelectedGenre = new Command<Picker>(vm => GenreCompleted(vm));
            SelectedActor = new Command<Picker>(vm => ActorCompleted(vm));
            SelectedFilmMaker = new Command<Picker>(vm => FilmMakerCompleted(vm));
        }

        private async Task PopulatePickers()
        {
            ActorsCastAvailable = await App.actorService.GETList();
            FilmMakersAvailable = await App.filmMakerService.GETList();
            ActorsCastInserted = new ObservableCollection<Actor>();

            //Method SetData must not be awaited, so we use this syntax to remove the warning
            var _ = Task.Run(() => SetData());
        }

        private async Task SetData()
        {
            if(Film != null || FilmMakersAvailable !=null || ActorsCastAvailable != null)
            {
                //Overwrite empty Entry and GenrePicker
                Id = Film._id;
                Title = Film.title;
                Year = Film.year.ToString();
                Genre = Film.genre;

                //Overwrite empty FilmMakerPicker
                for (int i = 0; i < FilmMakersAvailable.Count; i++)
                {
                    if (FilmMakersAvailable[i]._id.Equals(Film.filmMaker))
                    {
                        FilmMaker = FilmMakersAvailable[i];
                    }                 
                }

                //Add Id of Actor (Film.cast) in ActorCastId
                for (int j = 0; j < Film.cast.Length; j++)
                {
                    ActorCastId.Add(Film.cast[j]);
                    ActorsCastInserted.Add(await App.actorService.GETId(Film.cast[j]));
                }
                
                //Remove from ActorCastAvailable all the Actors which are already inserted
                for(int k = 0; k < ActorsCastAvailable.Count; k++)
                {
                    for(int h = 0; h<ActorCastId.Count; h++)
                    {
                        if (ActorCastId[h].Equals(ActorsCastAvailable[k].Id))
                        {
                            ActorsCastAvailable.Remove(ActorsCastAvailable[k]);
                            k = 0;
                            h = 0;
                        }
                    }
                }
                IsPresent = true;
            }
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
           Actor = (Actor)picker.SelectedItem;
            if (Actor != null)
            {
                ActorsCastInserted.Add(Actor);
                ActorCastId.Add(Actor.Id);
                bool found = false;
                int iterator = 0;
                while(iterator < ActorsCastAvailable.Count && !found)
                {
                    if (Actor.Id.Equals(ActorsCastAvailable[iterator].Id))
                    {
                        found = true;
                    }
                    iterator++;
                }

                //DO NOT TOUCH NEVER!
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
            Film film = new Film();
            film.title = Title;
            film.year = Int32.Parse(Year);
            film.genre = Genre;

            List<string> supportList = new List<string>(ActorCastId);
            film.cast = supportList.ToArray();

            film.filmMaker = FilmMaker.Id;

            if (IsPresent)
            {
                film._id = Film._id;
                await App.filmService.PUT(film);
            }
            else
                await App.filmService.POST(film);

            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
        }
    }
}

using angular6.Models;
using angular6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace angular6.ViewModels.ResourcesViewModel
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

        
        private String _genre;
        public String Genre
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

        
        private FilmMaker _filmmaker;
        public  FilmMaker  FilmMaker
        {
            get
            {
                return _filmmaker;
            }
            set
            {
                SetValue(ref _filmmaker, value);
            }
        }
        
        private ObservableCollection<FilmMaker> _filmmakersAvailable;
        //This is the collection of FilmMakers that can be inserted as filmmaker for the 
        public ObservableCollection<FilmMaker> FilmMakersAvailable
        {
            get
            {
                return _filmmakersAvailable;
            }
            set
            {
                SetValue(ref _filmmakersAvailable, value);
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

        
        private ObservableCollection<Actor> _castInserted;
        //This is the collection of actors that are ALREADY inserted as cast members for the 
        public ObservableCollection<Actor> CastInserted
        {
            get
            {
                return _castInserted;
            }
            set
            {
                SetValue(ref _castInserted, value);
            }
        }

        private ObservableCollection<Actor> _castAvailable;
        //This is the collection of actors that CAN BE inserted as cast members for the 
        public ObservableCollection<Actor> CastAvailable
        {
            get
            {
                return _castAvailable;
            }
            set
            {
                SetValue(ref _castAvailable, value);
            }
        }
        private ObservableCollection<Test> _testInserted;
        //This is the collection of tests that are ALREADY inserted as test members for the 
        public ObservableCollection<Test> TestInserted
        {
            get
            {
                return _testInserted;
            }
            set
            {
                SetValue(ref _testInserted, value);
            }
        }

        private ObservableCollection<Test> _testAvailable;
        //This is the collection of tests that CAN BE inserted as test members for the 
        public ObservableCollection<Test> TestAvailable
        {
            get
            {
                return _testAvailable;
            }
            set
            {
                SetValue(ref _testAvailable, value);
            }
        }
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        
        public ICommand TitleCompletedCommand { get; private set; }
        
        public ICommand YearCompletedCommand { get; private set; }
        
        
        public ICommand SelectedGenreCommand { get; private set; }
        
        
        public ICommand SelectedFilmMakerCommand { get; private set; }
        

        public ICommand SetDataForEditingCommand { get; private set; }
        
        public ICommand SelectedCastCommand { get; private set; }
        public ICommand DeleteItemCastCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (e as Actor);
                    int i = 0 ;
                    bool found = false;
                    while (i <CastInserted.Count && !found)
                    {
                        if (item.Id.Equals(CastInserted[i].Id))
                        {
                            CastAvailable.Add(CastInserted[i]);
                            CastInserted.RemoveAt(CastInserted.IndexOf(CastInserted[i]));
                            found = true;
                        }
                        i++;
                    }
                });
            }
        }
        public ICommand SelectedTestCommand { get; private set; }
        public ICommand DeleteItemTestCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (e as Test);
                    int i = 0 ;
                    bool found = false;
                    while (i <TestInserted.Count && !found)
                    {
                        if (item.Id.Equals(TestInserted[i].Id))
                        {
                            TestAvailable.Add(TestInserted[i]);
                            TestInserted.RemoveAt(TestInserted.IndexOf(TestInserted[i]));
                            found = true;
                        }
                        i++;
                    }
                });
            }
        }
        #endregion

        public FilmEditViewModel(Film filmToEdit, ObservableCollection<Actor> actors, ObservableCollection<Test> tests)
        {
            Film = filmToEdit;

            SetDataForEditingCommand = new Command(async vm => await SetData());
            
            //Remove from actors all the elements that are null
            if(actors != null){
                for(int i = 0; i<actors.Count; i++)
                {
                    if (actors[i] == null)
                    {
                        actors.RemoveAt(i);
                        i--;
                    }
                }
            }
            CastInserted = actors;
            SelectedCastCommand = new Command<Picker>(vm =>ActorCompleted(vm));
            //Remove from tests all the elements that are null
            if(tests != null){
                for(int i = 0; i<tests.Count; i++)
                {
                    if (tests[i] == null)
                    {
                        tests.RemoveAt(i);
                        i--;
                    }
                }
            }
            TestInserted = tests;
            SelectedTestCommand = new Command<Picker>(vm =>TestCompleted(vm));
            SaveCommand = new Command(async vm => await SaveFilmData());
            BackCommand = new Command(async vm => await GoBack());
            
            TitleCompletedCommand = new Command<Entry>(vm => TitleEntryCompleted(vm));
            
            YearCompletedCommand = new Command<Entry>(vm => YearEntryCompleted(vm));
            
            
            
            SelectedGenreCommand = new Command<Picker>(vm => GenreCompleted(vm));
            
            
            SelectedFilmMakerCommand = new Command<Picker>(vm => FilmMakerCompleted(vm));
            
        }
        
        private async Task SetData()
        {
            
            CastAvailable = await App.actorService.GETList();
            
            TestAvailable = await App.testService.GETList();
            
            
            
            FilmMakersAvailable = await App.filmmakerService.GETList();
            

            if (Film != null)
            {
                IsPresent = true;
                //Overwrite entries
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

                
                //Remove from CastAvailable all the Actor which are already inserted
                for (int k = 0; k < CastAvailable.Count; k++)
                {
                    for (int h = 0; h <CastInserted.Count; h++)
                    {
                        if(CastAvailable.Count != 0)
                        {
                            if (CastInserted[h].Id.Equals(CastAvailable[k].Id))
                            {
                                CastAvailable.Remove(CastAvailable[k]);
                                k = 0;
                                h = -1;
                            }
                        }
                    }
                }
                //Remove from TestAvailable all the Test which are already inserted
                for (int k = 0; k < TestAvailable.Count; k++)
                {
                    for (int h = 0; h <TestInserted.Count; h++)
                    {
                        if(TestAvailable.Count != 0)
                        {
                            if (TestInserted[h].Id.Equals(TestAvailable[k].Id))
                            {
                                TestAvailable.Remove(TestAvailable[k]);
                                k = 0;
                                h = -1;
                            }
                        }
                    }
                }
            }
            else
            {
                Film = new Film();
                 
                CastInserted = new ObservableCollection<Actor>();
                
                TestInserted = new ObservableCollection<Test>();
                
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
           Actor actorSelected = (Actor)picker.SelectedItem;
            if (actorSelected != null)
            {
                CastInserted.Add(actorSelected);
                bool found = false;
                int iterator = 0;
                while(iterator < CastAvailable.Count && !found)
                {
                    if (actorSelected.Id.Equals(CastAvailable[iterator].Id))
                    {
                        found = true;
                    }
                    iterator++;
                }

                //DO NOT TOUCH
                //This allows to modify the ItemSource of the Picker dynamically where actors can be selected
                ObservableCollection<Actor> support = new ObservableCollection<Actor>(CastAvailable);
                support.RemoveAt(iterator-1);
                CastAvailable = support;
            }            
        }
        private void TestCompleted(Picker picker)
        {
           Test testSelected = (Test)picker.SelectedItem;
            if (testSelected != null)
            {
                TestInserted.Add(testSelected);
                bool found = false;
                int iterator = 0;
                while(iterator < TestAvailable.Count && !found)
                {
                    if (testSelected.Id.Equals(TestAvailable[iterator].Id))
                    {
                        found = true;
                    }
                    iterator++;
                }

                //DO NOT TOUCH
                //This allows to modify the ItemSource of the Picker dynamically where actors can be selected
                ObservableCollection<Test> support = new ObservableCollection<Test>(TestAvailable);
                support.RemoveAt(iterator-1);
                TestAvailable = support;
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

            
                film.Title = Title;
            
            
                film.Year = Int32.Parse(Year);
            
            
                film.Genre = Genre;
            

            
                List<string> supportActorList = new List<string>();
                foreach (Actor a in CastInserted)
                    supportActorList.Add(a.Id);
                film.Cast = supportActorList.ToArray();
            
                List<string> supportTestList = new List<string>();
                foreach (Test a in TestInserted)
                    supportTestList.Add(a.Id);
                film.Cast = supportTestList.ToArray();
            
            
                film.FilmMaker = FilmMaker.Id;
            
                if (IsPresent)
                {
                    film.Id = Film.Id;
                    await App.filmService.PUT(film);
                }
                else
                    await App.filmService.POST(film);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();   
        }
    }
}

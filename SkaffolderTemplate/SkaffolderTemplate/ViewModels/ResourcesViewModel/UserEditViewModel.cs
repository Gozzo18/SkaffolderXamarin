using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views.List;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels.ResourcesViewModel
{
    public class UserEditViewModel : BaseViewModel
    {
        #region Attributes and Properties
        
        private string _mail;
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                SetValue(ref _mail, value);
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetValue(ref _name, value);
            }
        }
        private string _roles;
        public string Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                SetValue(ref _roles, value);
            }
        }
        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                SetValue(ref _surname, value);
            }
        }
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetValue(ref _username, value);
            }
        }

        

        private bool _isPresent;
        //True = editing User, False = creating new User
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

        

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                SetValue(ref _user, value);
            }
        }

        
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SetDataForEditingCommand { get; private set; }
        
        public ICommand MailCompletedCommand { get; private set; }
        
        public ICommand NameCompletedCommand { get; private set; }
        
        public ICommand RolesCompletedCommand { get; private set; }
        
        public ICommand SurnameCompletedCommand { get; private set; }
        
        public ICommand UsernameCompletedCommand { get; private set; }
        
        
        

        
        #endregion

        public UserEditViewModel(User userToEdit)
        {
            User = userToEdit;

            
            SetDataForEditingCommand = new Command(async vm => await SetData());
            SaveCommand = new Command(async vm => await SaveFilmData());
            BackCommand = new Command(async vm => await GoBack());
            
            MailCompletedCommand = new Command<Entry>(vm => MailEntryCompleted(vm));
            
            NameCompletedCommand = new Command<Entry>(vm => NameEntryCompleted(vm));
            
            RolesCompletedCommand = new Command<Entry>(vm => RolesEntryCompleted(vm));
            
            SurnameCompletedCommand = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            
            UsernameCompletedCommand = new Command<Entry>(vm => UsernameEntryCompleted(vm));
            
            
            
            
        }

        private async Task SetData()
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
            {
                Film = new Film();
                ActorsCastInserted = new ObservableCollection<Actor>();
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

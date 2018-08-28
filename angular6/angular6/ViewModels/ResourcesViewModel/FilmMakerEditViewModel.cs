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
    public class FilmMakerEditViewModel : BaseViewModel
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

        

        

        private bool _isPresent;
        //True = editing FilmMaker, False = creating new FilmMaker
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
        public FilmMaker FilmMaker
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

        
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        
        public ICommand NameCompletedCommand { get; private set; }
        
        public ICommand SurnameCompletedCommand { get; private set; }
        
        
        

        
        #endregion

        public FilmMakerEditViewModel(FilmMaker filmmakerToEdit)
        {
            FilmMaker = filmmakerToEdit;
            
            if(FilmMaker != null)
            {
                Name = FilmMaker.Name;
                Surname = FilmMaker.Surname;
                IsPresent = true;
            }
            

            
            SaveCommand = new Command(async vm => await SaveFilmMakerData());
            BackCommand = new Command(async vm => await GoBack());
            
            NameCompletedCommand = new Command<Entry>(vm => NameEntryCompleted(vm));
            
            SurnameCompletedCommand = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            
            
            
            
        }
        

        
        private void NameEntryCompleted(Entry FilmMakerName)
        {
            Name = FilmMakerName.Text;
        }
        private void SurnameEntryCompleted(Entry FilmMakerSurname)
        {
            Surname = FilmMakerSurname.Text;
        }

        

        

        

        

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();    
            
        }

        private async Task SaveFilmMakerData()
        {
            FilmMaker filmmaker = new FilmMaker();

            
                filmmaker.Name = Name;
            
                filmmaker.Surname = Surname;
            
            
            

            
            
                if (IsPresent)
                {
                    filmmaker.Id = FilmMaker.Id;
                    await App.filmmakerService.PUT(filmmaker);
                }
                else
                    await App.filmmakerService.POST(filmmaker);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();   
        }
    }
}

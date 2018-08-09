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
        public ICommand SetDataForEditingCommand { get; private set; }
        
        public ICommand NameCompletedCommand { get; private set; }
        
        public ICommand SurnameCompletedCommand { get; private set; }
        
        
        

        
        #endregion

        public FilmMakerEditViewModel(FilmMaker filmmakerToEdit)
        {
            FilmMaker = filmmakerToEdit;

            
            SetDataForEditingCommand = new Command(async vm => await SetData());
            SaveCommand = new Command(async vm => await SaveFilmMakerData());
            BackCommand = new Command(async vm => await GoBack());
            
            NameCompletedCommand = new Command<Entry>(vm => NameEntryCompleted(vm));
            
            SurnameCompletedCommand = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            
            
            
            
            
        }

        private async Task SetData()
        {
            
            
            

            if (FilmMaker != null)
            {
                //Overwrite entries
                Id = FilmMaker.Id;
                
                Name = FilmMaker.Name;
                
                Surname = FilmMaker.Surname;
                

                

                

                
                IsPresent = true;
            }
            else
            {
                FilmMaker = new FilmMaker();
                 
            }
                
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

            
                FilmMaker.Name = Name;
            
                FilmMaker.Surname = Surname;
            
            
            

            
            
                if (IsPresent)
                {
                    FilmMaker.Id = FilmMaker.Id;
                    await App.filmmakerService.PUT(FilmMaker);
                }
                else
                    await App.filmmakerService.POST(FilmMaker);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();   
        }
    }
}

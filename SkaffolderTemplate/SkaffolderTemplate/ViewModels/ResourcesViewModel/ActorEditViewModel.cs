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
    public class ActorEditViewModel : BaseViewModel
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

        
        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                SetValue(ref _birthDate, value);
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
        //True = editing Actor, False = creating new Actor
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

        
        #endregion

        #region Commands
        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SetDataForEditingCommand { get; private set; }
        
        public ICommand BirthDateCompletedCommand { get; private set; }
        
        public ICommand NameCompletedCommand { get; private set; }
        
        public ICommand SurnameCompletedCommand { get; private set; }
        
        
        

        
        #endregion

        public ActorEditViewModel(Actor actorToEdit)
        {
            Actor = actorToEdit;

            
            SetDataForEditingCommand = new Command(async vm => await SetData());
            SaveCommand = new Command(async vm => await SaveActorData());
            BackCommand = new Command(async vm => await GoBack());
            
            NameCompletedCommand = new Command<Entry>(vm => NameEntryCompleted(vm));
            
            SurnameCompletedCommand = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            
            
            BirthDateCompletedCommand = new Command<DateTime>(vm => BirthDatePickerSelected(vm));
            
            
            
            
        }

        private async Task SetData()
        {
            
            
            

            if (Actor != null)
            {
                //Overwrite entries
                Id = Actor.Id;
                
                BirthDate = Actor.BirthDate;
                
                Name = Actor.Name;
                
                Surname = Actor.Surname;
                

                

                

                
                IsPresent = true;
            }
            else
            {
                Actor = new Actor();
                 
            }
                
        }

        
        private void NameEntryCompleted(Entry ActorName)
        {
            Name = ActorName.Text;
        }
        private void SurnameEntryCompleted(Entry ActorSurname)
        {
            Surname = ActorSurname.Text;
        }

        
        private void BirthDatePickerSelected(DateTime ActorBirthDate)
        {
            BirthDate = ActorBirthDate;
        }

        

        

        

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();    
            
        }

        private async Task SaveActorData()
        {

            
                Actor.BirthDate = BirthDate;
            
                Actor.Name = Name;
            
                Actor.Surname = Surname;
            
            
            

            
            
                if (IsPresent)
                {
                    Actor.Id = Actor.Id;
                    await App.actorService.PUT(Actor);
                }
                else
                    await App.actorService.POST(Actor);

                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PopAsync();   
        }
    }
}

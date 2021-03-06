﻿using SkaffolderTemplate.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
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
        #endregion

        #region Commands
        public ICommand SaveCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand NameCompletedCommand { get; private set; }
        public ICommand SurnameCompletedCommand { get; private set; }
        public ICommand BirthDateCompletedCommand { get; private set; }
        #endregion

        public ActorEditViewModel(Actor actorToEdit)
        {
            //If it's the editing case
            if (actorToEdit != null)
            {
                Id = actorToEdit.Id;
                Name = actorToEdit.Name;
                Surname = actorToEdit.Surname;
                BirthDate = actorToEdit.BirthDate;
                IsPresent = true;
            }
            
            SaveCommand = new Command(async vm => await SaveActorData());
            BackCommand = new Command(async vm => await GoBack());
            NameCompletedCommand = new Command<Entry>(vm => NameEntryCompleted(vm));
            SurnameCompletedCommand = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            BirthDateCompletedCommand = new Command<DateTime>(vm => BirthDatePickerSelected(vm));
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
            Actor actor = new Actor();
            actor.Name = Name;
            actor.Surname = Surname;
            actor.BirthDate = BirthDate;

            if (IsPresent)
            {
                actor.Id = Id;
                await App.actorService.PUT(actor);
            }
            else
                await App.actorService.POST(actor);

            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
        }
    }
}

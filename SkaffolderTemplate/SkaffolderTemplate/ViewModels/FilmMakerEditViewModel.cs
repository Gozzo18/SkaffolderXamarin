﻿using SkaffolderTemplate.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
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
        #endregion

        #region Commands
        public ICommand SaveCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand NameCompletedCommand { get; private set; }
        public ICommand SurnameCompletedCommand { get; private set; }
        #endregion

        public FilmMakerEditViewModel(FilmMaker filmMakerToEdit)
        {
            //If it's the editing case
            if (filmMakerToEdit != null)
            {
                Name = filmMakerToEdit.Name;
                Surname = filmMakerToEdit.Surname;
                Id = filmMakerToEdit.Id;
                IsPresent = true;
            }

            SaveCommand = new Command(async vm => await SaveActorData());
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

        private async Task SaveActorData()
        {
            FilmMaker filmMaker = new FilmMaker();
            filmMaker.Name = Name;
            filmMaker.Surname = Surname;
            
            if (IsPresent)
            {
                filmMaker.Id = Id;
                await App.filmMakerService.PUT(filmMaker);
            }
            else
                await App.filmMakerService.POST(filmMaker);

            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();

        }
    }
}

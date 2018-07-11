﻿using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Text;
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
        #endregion

        private readonly IPageService _pageService;

        #region Command
        public ICommand Save { get; private set; }
        public ICommand Back { get; private set; }
        public ICommand SetPreviewsValue { get; private set; }
        public ICommand NameCompleted { get; private set; }
        public ICommand SurnameCompleted { get; private set; }
        #endregion

        public FilmMakerEditViewModel(FilmMaker alreadyPresentFilmMaker, IPageService pageService)
        {
            FilmMaker = alreadyPresentFilmMaker;
            _pageService = pageService;
            Save = new Command(async vm => await SaveActorData());
            Back = new Command(async vm => await GoBack());
            SetPreviewsValue = new Command(SetData);
            NameCompleted = new Command<Entry>(vm => NameEntryCompleted(vm));
            SurnameCompleted = new Command<Entry>(vm => SurnameEntryCompleted(vm));
        }

        private void SetData()
        {
            if (FilmMaker != null)
            {
                Id = FilmMaker._id;
                Name = FilmMaker.name;
                Surname = FilmMaker.surname;
                IsPresent = true;
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
            await _pageService.PopAsync();
        }

        private async Task SaveActorData()
        {
            FilmMaker fm = new FilmMaker();
            fm.name = Name;
            fm.surname = Surname;
            
            if (IsPresent)
            {
                fm._id = FilmMaker._id;
                await App.filmMakerService.PUT(fm);
            }
            else
                await App.filmMakerService.POST(fm);

            await _pageService.PushAsync(new FilmMakerPage(), false);

        }
    }
}

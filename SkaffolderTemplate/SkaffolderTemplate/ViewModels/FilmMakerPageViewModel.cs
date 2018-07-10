using SkaffolderTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SkaffolderTemplate.ViewsForm;
using System.Threading.Tasks;

namespace SkaffolderTemplate.ViewModels
{
    public class FilmMakerPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<FilmMaker> _filmMakersList;
        public ObservableCollection<FilmMaker> FilmMakersList
        {
            get
            {
                return _filmMakersList;
            }
            set
            {
                SetValue(ref _filmMakersList, value);
            }
        }

        private bool _refreshing;
        public bool Refreshing
        {
            get
            {
                return _refreshing;
            }
            set
            {
                SetValue(ref _refreshing, value);
            }
        }
        #endregion

        private readonly IPageService _pageService;

        #region Commands
        public ICommand Add { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand SelectedFilmMaker { get; private set; }
        public ICommand LoadData { get; private set; }
        #endregion

        public FilmMakerPageViewModel(PageService pageService)
        {
            _pageService = pageService;
            Add = new Command(async vm => await AddNewFilmMaker());
            Refresh = new Command(async vm => await RefreshList());
            SelectedFilmMaker = new Command<FilmMaker>(async vm => await SelectedItem(vm));
            LoadData = new Command<ObservableCollection<FilmMaker>>(async vm => await GetRequest());
        }

        private async Task SelectedItem(FilmMaker filmMaker)
        {
            var choose = await _pageService.DisplayActionSheet("Do you want to Delete or Edit this film maker?", "Cancel", "Delete", "Edit");
            if (choose.Equals("Delete"))
            {
                await App.filmMakerService.DELETE(filmMaker._id);
                await RefreshList();
            }
            else if (choose.Equals("Edit"))
            {
                await _pageService.PushAsync(new FilmMakerEdit(filmMaker), false);
                return;
            }
        }

        private async Task GetRequest()
        {
            FilmMakersList = await App.filmMakerService.GETList();
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            FilmMakersList = await App.filmMakerService.GETList();
            Refreshing = false;
        }

        private async Task AddNewFilmMaker()
        {
            await _pageService.PushAsync(new FilmMakerEdit(null), false);
        }
    }
}

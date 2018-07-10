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
        private ObservableCollection<FilmMaker> _listaDiFilmMakers;
        public ObservableCollection<FilmMaker> LISTADIFILMMAKER
        {
            get
            {
                return _listaDiFilmMakers;
            }
            set
            {
                SetValue(ref _listaDiFilmMakers, value);
            }
        }

        private bool _refreshing = false;
        public bool REFRESHING
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

        private readonly IPageService _pageService;

        public FilmMakerPageViewModel(ObservableCollection<FilmMaker> lis, PageService pageService)
        {
            LISTADIFILMMAKER = lis;
            _pageService = pageService;
        }

        public async Task SelectedItem(FilmMaker selectedFilmMaker, string scelta)
        {

            if (scelta.Equals("Eliminare"))
                await App.filmMakerService.DELETE(selectedFilmMaker._id);
            else if (scelta.Equals("Modifica"))
                await _pageService.PushAsync(new FilmMakerEdit(null), false);

            RefreshList();
        }

        public async void RefreshList()
        {
            REFRESHING = true;
            LISTADIFILMMAKER = await App.filmMakerService.GETList();
            REFRESHING = false;
        }

        public async Task AggiungiFilmMaker()
        {
            await _pageService.PushAsync(new FilmMakerEdit(null), false);
        }

    }
}

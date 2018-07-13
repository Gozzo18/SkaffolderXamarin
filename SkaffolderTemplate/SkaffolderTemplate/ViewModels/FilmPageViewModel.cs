using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using SkaffolderTemplate.ViewsForm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class FilmPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<Film> _filmsList;
        public ObservableCollection<Film> FilmsList
        {
            get
            {
                return _filmsList;
            }
            set
            {
                SetValue(ref _filmsList, value);
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

        #region Command
        public ICommand Add { get; private set; }
        public ICommand Refresh { get; private set; }
        public ICommand SelectedFilm { get; private set; }
        public ICommand LoadData { get; private set; }
        public ICommand ActorPage { get; private set; }
        public ICommand FilmMakerPage { get; private set; }
        #endregion

        public FilmPageViewModel(PageService pageService)
        {
            _pageService = pageService;
            Add = new Command(async vm => await AddNewFilm());
            Refresh = new Command(async vm => await RefreshList());
            SelectedFilm = new Command<Film>(async vm => await SelectedItem(vm));
            LoadData = new Command<ObservableCollection<Film>>(async vm => await GetRequest());
            ActorPage = new Command(async vm => await GoToActorPage());
            FilmMakerPage = new Command(async vm => await GoToFilmMakerPage());
        }

        private async Task SelectedItem(Film film)
        {
            var choose = await _pageService.DisplayActionSheet("Do you want to Delete or Edit this film?", "Cancel", "Delete", "Edit");
            if (choose.Equals("Delete"))
            {
                await App.filmService.DELETE(film._id);
                await RefreshList();
            }
            else if (choose.Equals("Edit"))
            {
                await _pageService.PushAsync(new FilmEdit(film), false);
                return;
            }
        }

        private async Task GetRequest()
        {
            FilmsList = await App.filmService.GETList();
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            FilmsList = await App.filmService.GETList();
            Refreshing = false;
        }

        private async Task AddNewFilm()
        {
            await _pageService.PushAsync(new FilmEdit(null), false);
        }

        private async Task GoToActorPage()
        {
            await _pageService.PushAsync(new ActorPage(), false);
        }

        private async Task GoToFilmMakerPage()
        {
            await _pageService.PushAsync(new FilmMakerPage(), false);
        }
    }
}

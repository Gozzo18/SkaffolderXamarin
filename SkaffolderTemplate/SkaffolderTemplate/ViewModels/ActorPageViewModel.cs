using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewsForm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class ActorPageViewModel : BaseViewModel
    {
        #region Attributes and Properties
        private ObservableCollection<Actor> _actorList;
        public ObservableCollection<Actor> ActorsList
        {
            get
            {
                return _actorList;
            }
            set
            {
                SetValue(ref _actorList, value);
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
        public ICommand SelectedActor { get; private set; }
        public ICommand LoadData { get; private set; }
        #endregion

        public ActorPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Add = new Command(async vm => await AddNewActor());
            Refresh = new Command(async vm => await RefreshList());
            SelectedActor = new Command<Actor>(async vm => await SelectedItem(vm));
            LoadData = new Command<ObservableCollection<Actor>>(async vm => await GetRequest());
        }

        private async Task RefreshList()
        {
            Refreshing = true;
            ActorsList = await App.actorService.GETList();
            Refreshing = false;
        }

        private async Task SelectedItem(Actor actor)
        {
            var choose = await _pageService.DisplayActionSheet("Do you want to Delete or Edit this actor?", "Cancel", "Delete", "Edit");
            if (choose.Equals("Delete"))
            {
                await App.actorService.DELETE(actor._id);
                await RefreshList();
            }
            else if(choose.Equals("Edit"))
            {
                await _pageService.PushAsync(new ActorEdit(actor), false);
                return;
            }
        }

        private async Task AddNewActor()
        {
            await _pageService.PushAsync(new ActorEdit(null), false);
        }

        private async Task GetRequest()
        {
            ActorsList = await App.actorService.GETList();
        }
    }
}

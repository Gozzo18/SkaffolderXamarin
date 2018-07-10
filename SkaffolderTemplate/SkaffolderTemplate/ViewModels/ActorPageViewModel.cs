using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewsForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SkaffolderTemplate.ViewModels
{
    public class ActorPageViewModel : BaseViewModel
    {

        private ObservableCollection<Actor> _listaDiAttori;
        public ObservableCollection<Actor> LISTADIATTORI
        {
            get
            {
                return _listaDiAttori;
            }
            set
            {
                SetValue(ref _listaDiAttori, value);
            }
        }

        private bool _refreshing;
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

        public ActorPageViewModel(ObservableCollection<Actor> lis, IPageService pageService)
        {
            LISTADIATTORI = lis;
            _pageService = pageService;
        }

        public async void RefreshList()
        {
            REFRESHING = true;
            LISTADIATTORI = await App.actorService.GETList();
            REFRESHING = false;
        }

        public async Task SelectedItem(Actor actorSelected, string scelta)
        {

            if (scelta.Equals("Eliminare"))
            {
                await App.actorService.DELETE(actorSelected._id);
                RefreshList();
            }
            else if(scelta.Equals("Modifica"))
            {
                await _pageService.PushAsync(new ActorEdit(actorSelected), false);
                return;
            }
        }

        public async Task AggiungiAttore()
        {
            await _pageService.PushAsync(new ActorEdit(null), false);
        }



    }
}

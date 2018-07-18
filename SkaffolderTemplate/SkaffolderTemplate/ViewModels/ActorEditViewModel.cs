using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
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

        #region Commands
        public ICommand Save { get; private set; }
        public ICommand Back { get; private set; }
        public ICommand SetPreviewsValue { get; private set; }
        public ICommand NameCompleted { get; private set; }
        public ICommand SurnameCompleted { get; private set; }
        public ICommand BirthDateCompleted { get; private set; }
        #endregion

        public ActorEditViewModel(Actor alreadyPresentActor)
        {
            Actor = alreadyPresentActor;
            Save = new Command(async vm => await SaveActorData());
            Back = new Command(async vm => await GoBack());
            SetPreviewsValue = new Command(SetData);
            NameCompleted = new Command<Entry>(vm => NameEntryCompleted(vm));
            SurnameCompleted = new Command<Entry>(vm => SurnameEntryCompleted(vm));
            BirthDateCompleted = new Command<DateTime>(vm => BirthDatePickerSelected(vm));
        }

        private void SetData()
        {
            if(Actor != null)
            {
                Id = Actor._id; 
                Name = Actor.name;
                Surname = Actor.surname;
                BirthDate = Actor.BirthDate;
                IsPresent = true;
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

        private void BirthDatePickerSelected(DateTime birthDate)
        {
            BirthDate = birthDate;
        }

        private async Task GoBack()
        {
            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
        }

        private async Task SaveActorData()
        {
            Actor actor = new Actor();
            actor.name = Name;
            actor.surname = Surname;
            actor.birthDate = BirthDate;

            if (IsPresent)
            {
                actor._id = Actor._id;
                await App.actorService.PUT(actor);
            }
            else
                await App.actorService.POST(actor);

            var masterDetailPage = App.Current.MainPage as MasterDetailPage;
            await masterDetailPage.Detail.Navigation.PopAsync();
        }

    }
}

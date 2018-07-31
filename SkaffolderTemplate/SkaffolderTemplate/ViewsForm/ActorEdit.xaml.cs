using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorEdit : ContentPage
	{
        //Set ViewModel for BindingContext
        private ActorEditViewModel ViewModel
        {
            get
            {
                return BindingContext as ActorEditViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public ActorEdit(Actor actor)
		{
            //Setting BindingContext
            ViewModel = new ActorEditViewModel(actor);
			InitializeComponent ();
		}

        private void ActorNameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompletedCommand.Execute(sender as Entry);
        }

        private void ActorSurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompletedCommand.Execute(sender as Entry);
        }

        private void BirthDate_Selected(object sender, DateChangedEventArgs e)
        {
            ViewModel.BirthDateCompletedCommand.Execute(e.NewDate);
        }
    }
}
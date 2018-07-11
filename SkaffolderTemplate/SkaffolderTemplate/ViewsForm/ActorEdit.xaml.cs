using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using SkaffolderTemplate.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorEdit : ContentPage
	{
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
            ViewModel = new ActorEditViewModel(actor, new PageService());
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.SetPreviewsValue.Execute(null);           
        }

        private void ActorNameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompleted.Execute(sender as Entry);
        }

        private void ActorSurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompleted.Execute(sender as Entry);
        }

        private void BirthDate_Selected(object sender, DateChangedEventArgs e)
        {
            ViewModel.BirthDateCompleted.Execute(e.NewDate);
        }
    }
}
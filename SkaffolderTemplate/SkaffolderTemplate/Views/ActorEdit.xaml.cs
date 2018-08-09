using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels.ResourcesViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
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

        public ActorEdit (Actor actor)
		{
            //Setting BindingContext
            ViewModel = new ActorEditViewModel(actor);
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            //Remove from navigation stack the LoadingView
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2 ]);

            base.OnAppearing();
            
        }

        
        private void BirthDateEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.BirthDateCompletedCommand.Execute(sender as Entry);
        }
        private void NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompletedCommand.Execute(sender as Entry);
        }
        private void SurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompletedCommand.Execute(sender as Entry);
        }

        

        

        
    }
}
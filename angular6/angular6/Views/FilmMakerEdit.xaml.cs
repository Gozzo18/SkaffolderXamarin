using angular6.Models;
using angular6.ViewModels.ResourcesViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmMakerEdit : ContentPage
    {
        //Set ViewModel for BindingContext
        private FilmMakerEditViewModel ViewModel
        {
            get
            {
                return BindingContext as FilmMakerEditViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public FilmMakerEdit (FilmMaker filmmaker)
		{
            //Setting BindingContext
            ViewModel = new FilmMakerEditViewModel(filmmaker);
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            //Remove from navigation stack the LoadingView
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2 ]);

            base.OnAppearing();
            
            //Set the ItemSource for all the Pickers
            ViewModel.SetDataForEditingCommand.Execute(null);
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
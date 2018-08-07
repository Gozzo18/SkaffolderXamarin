using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorsList : ContentPage
	{
        //Set ViewModel for BindingContext
        private ActorsListViewModel ViewModel
        {
            get
            {
                return BindingContext as ActorsListViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }
        
		public ActorsList ()
		{
            //Setting BindingContext
            ViewModel = new ActorsListViewModel();
            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadDataCommand.Execute(null);           
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchCommand.Execute(null);
        }

        //Remove graphic effect on ListView
        private void MainListOfActors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
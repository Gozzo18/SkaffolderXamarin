using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsersList : ContentPage
	{
        //Set ViewModel for BindingContext
        private UserListViewModel ViewModel
        {
            get
            {
                return BindingContext as UserListViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }
        
		public UserList ()
		{
            //Setting BindingContext
            ViewModel = new UserListViewModel();
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
        private void MainListOfFilmMakers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
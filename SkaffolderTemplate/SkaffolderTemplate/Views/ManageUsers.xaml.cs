﻿using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManageUsers : ContentPage
	{
        //Set ViewModel for BindingContext
        private ManageUsersViewModel ViewModel
        {
            get
            {
                return BindingContext as ManageUsersViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public ManageUsers ()
		{
            //Setting BindingContext
            BindingContext = new ManageUsersViewModel();
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

        //Hide graphic effect on ListView
        private void MainListOfUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
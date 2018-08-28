using angular6.ViewModels.ResourcesViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestList : ContentPage
	{
        //Set ViewModel for BindingContext
        private TestListViewModel ViewModel
        {
            get
            {
                return BindingContext as TestListViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }
        
		public TestList ()
		{
            //Setting BindingContext
            ViewModel = new TestListViewModel();
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
        private void MainListOfTests_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
using SkaffolderTemplate.ViewModels.ResourcesViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerList : ContentPage
	{
        //Set ViewModel for BindingContext
        private FilmMakerListViewModel ViewModel
        {
            get
            {
                return BindingContext as FilmMakerListViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }
        
		public FilmMakerList ()
		{
            //Setting BindingContext
            ViewModel = new FilmMakerListViewModel();
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
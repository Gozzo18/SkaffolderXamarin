using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmPage : ContentPage
	{
        //Set ViewModel for BindingContext
        private FilmPageViewModel ViewModel
        {
            get
            {
                return BindingContext as FilmPageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public FilmPage ()
		{
            //Setting BindingContext
            ViewModel = new FilmPageViewModel();
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            //Force garbace collector to run
            GC.Collect();
            GC.WaitForPendingFinalizers();

            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadDataCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchCommand.Execute(null);
        }

        //Remove graphic effect on ListView
        private void MainListOfFilms_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
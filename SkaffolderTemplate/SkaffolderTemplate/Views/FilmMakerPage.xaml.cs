using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerPage : ContentPage
	{
        //Set ViewModel for BindingContext
        private FilmMakerPageViewModel ViewModel
        {
            get
            {
                return BindingContext as FilmMakerPageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }
        
		public FilmMakerPage ()
		{
            //Setting BindingContext
            ViewModel = new FilmMakerPageViewModel();
            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadData.Execute(null);           
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
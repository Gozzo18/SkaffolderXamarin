using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerPage : ContentPage
	{
        //Set ViewModel for BindingContext
        public FilmMakerPageViewModel ViewModel
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
            ViewModel = new FilmMakerPageViewModel(new PageService());
            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadData.Execute(null);           
        }

        private void EditFilmMaker(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedFilmMaker.Execute(e.SelectedItem);
        }
    }
}
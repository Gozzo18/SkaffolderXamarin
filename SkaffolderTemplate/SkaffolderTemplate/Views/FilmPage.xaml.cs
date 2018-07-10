using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmPage : ContentPage
	{
        //Set ViewModel for BindingContext
        public FilmPageViewModel ViewModel
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
            ViewModel = new FilmPageViewModel(new PageService());
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadData.Execute(null);
        }

        private void EditFilm(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedFilm.Execute(e.SelectedItem);
        }
    }
}
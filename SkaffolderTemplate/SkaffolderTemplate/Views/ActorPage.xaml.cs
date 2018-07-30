using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorPage : ContentPage
	{
        //Set ViewModel for BindingContext
        private ActorPageViewModel ViewModel
        {
            get
            {
                return BindingContext as ActorPageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public ActorPage ()
		{
            //Setting BindingContext
            ViewModel = new ActorPageViewModel();
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

        //Hide graphic effect on ListView
        private void MainListOfActors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
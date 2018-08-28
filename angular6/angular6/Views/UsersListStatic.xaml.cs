using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using angular6.ViewModels;

namespace angular6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsersListStatic : ContentPage
    {
        //Set ViewModel for BindingContext
        private UsersListStaticViewModel ViewModel
        {
            get
            {
                return BindingContext as UsersListStaticViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public UsersListStatic()
        {
            //Setting BindingContext
            BindingContext = new UsersListStaticViewModel();
            InitializeComponent();
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
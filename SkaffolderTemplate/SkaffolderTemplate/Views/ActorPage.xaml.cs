using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorPage : ContentPage
	{
        //Set ViewModel for BindingContext
        public ActorPageViewModel ViewModel
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
            ViewModel = new ActorPageViewModel(new PageService());
            InitializeComponent ();            
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadData.Execute(null);
            
        }

        private void ActorEdit(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedActor.Execute(null);
        }
    }
}
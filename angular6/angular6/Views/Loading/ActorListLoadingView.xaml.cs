using angular6.Models;
using angular6.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorListLoadingView : ContentPage
	{
        private Actor actor;
        

		public ActorListLoadingView (Actor actorToEdit)
		{
            actor = actorToEdit;
            
			InitializeComponent();
		}

        //Load data that will be used by ActorEdit
        protected override async void OnAppearing()
        {
            if (actor != null)
            {
                
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new ActorEdit(actor), false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new ActorEdit(null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
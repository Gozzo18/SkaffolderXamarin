using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorEditLoadingView : ContentPage
	{
        private Actor actor;
        

		public ActorEditLoadingView (Actor actorToEdit)
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
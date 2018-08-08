using SkaffolderTemplate.Models;
using SkaffolderTemplate.Views.Edit;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserLoadingView : ContentPage
	{
        private User user;
        

		public LoadingView (User userToEdit)
		{
            user = userToEdit;
            
			InitializeComponent();
		}

        //Load data that will be used by UserEdit
        protected override async void OnAppearing()
        {
            if (user != null)
            {
                
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new UserEdit(user, false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new UserEdit(null, null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
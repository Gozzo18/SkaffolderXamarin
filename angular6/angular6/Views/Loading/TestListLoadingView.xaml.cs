using angular6.Models;
using angular6.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestListLoadingView : ContentPage
	{
        private Test test;
        

		public TestListLoadingView (Test testToEdit)
		{
            test = testToEdit;
            
			InitializeComponent();
		}

        //Load data that will be used by TestEdit
        protected override async void OnAppearing()
        {
            if (test != null)
            {
                
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new TestEdit(test), false);
            }
            else
            {
                var masterDetailPage = App.Current.MainPage as MasterDetailPage;
                await masterDetailPage.Detail.Navigation.PushAsync(new TestEdit(null), false);
            }
            this.OnDisappearing();
        
        }
    }
}
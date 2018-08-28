using angular6.Models;
using angular6.ViewModels.ResourcesViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestEdit : ContentPage
    {
        //Set ViewModel for BindingContext
        private TestEditViewModel ViewModel
        {
            get
            {
                return BindingContext as TestEditViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public TestEdit (Test test)
		{
            //Setting BindingContext
            ViewModel = new TestEditViewModel(test);
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            //Remove from navigation stack the LoadingView
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2 ]);

            base.OnAppearing();
            
            //Set the ItemSource for all the Pickers
            ViewModel.SetDataForEditingCommand.Execute(null);
        }

        
        private void NomeEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NomeCompletedCommand.Execute(sender as Entry);
        }

        
        

        

        
    }
}
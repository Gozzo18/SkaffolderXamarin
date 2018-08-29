using angular6.Models;
using angular6.ViewModels.ResourcesViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace angular6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmEdit : ContentPage
    {
        //Set ViewModel for BindingContext
        private FilmEditViewModel ViewModel
        {
            get
            {
                return BindingContext as FilmEditViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public FilmEdit (Film film, ObservableCollection<Actor> actors, ObservableCollection<Test> tests)
		{
            //Setting BindingContext
            ViewModel = new FilmEditViewModel(film, actors, tests);
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

        
        private void TitleEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.TitleCompletedCommand.Execute(sender as Entry);
        }
        private void YearEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.YearCompletedCommand.Execute(sender as Entry);
        }

        
        
        private void PickerGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.SelectedGenreCommand.Execute(sender as Picker);
        }

        
        private void PickerFilmMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
             ViewModel.SelectedFilmMakerCommand.Execute(sender as Picker); 
        }

        
        private void PickerCast_SelectedIndexChanged(object sender, EventArgs e)
        {
                ViewModel.SelectedCastCommand.Execute(sender as Picker);
                castInserted.HeightRequest += 15;
        }

        private void CastItemRemove_Clicked(object sender, EventArgs e)
        {
            castInserted.HeightRequest -= 30;
        }

        private void showCastPicker(object sender, EventArgs e)
        {
            castPicker.Focus();
        }

        //Hide graphic effect on ListView
        private void castInserted_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
        private void PickerTest_SelectedIndexChanged(object sender, EventArgs e)
        {
                ViewModel.SelectedTestCommand.Execute(sender as Picker);
                testInserted.HeightRequest += 15;
        }

        private void TestItemRemove_Clicked(object sender, EventArgs e)
        {
            testInserted.HeightRequest -= 30;
        }

        private void showTestPicker(object sender, EventArgs e)
        {
            testPicker.Focus();
        }

        //Hide graphic effect on ListView
        private void testInserted_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
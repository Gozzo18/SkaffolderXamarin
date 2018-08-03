using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.Edit
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

        public FilmEdit (Film film, ObservableCollection<Actor> actors)
		{
            //Setting BindingContext
            ViewModel = new FilmEditViewModel(film, actors);
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            //Remove from navigation stack the LoadingView
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2 ]);

            base.OnAppearing();
            //Set the ItemSource for all the Pickers
            ViewModel.SetPickersItemSourceCommand.Execute(null);
        }

        private void FilmTitleEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.TitleCompletedCommand.Execute(sender as Entry);
        }

        private void FilmYearEntry_Unfocused(object sender, FocusEventArgs e)
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

        private void PickerActor_SelectedIndexChanged(object sender, EventArgs e)
        {
                ViewModel.SelectedActorCommand.Execute(sender as Picker);
        }

        private void showActorPicker(object sender, EventArgs e)
        {
            actorPicker.Focus();
        }

        //Hide graphic effect on ListView
        private void castInserted_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
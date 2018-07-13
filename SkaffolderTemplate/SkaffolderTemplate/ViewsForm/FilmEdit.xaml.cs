using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using SkaffolderTemplate.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmEdit : ContentPage
    { 
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

        public FilmEdit (Film film)
		{
            ViewModel = new FilmEditViewModel(film, new PageService());
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.SetPickersItemSource.Execute(null);
        }

        private void FilmTitleEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.TitleCompleted.Execute(sender as Entry);
        }

        private void FilmYearEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.YearCompleted.Execute(sender as Entry);
        }

        private void PickerGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.SelectedGenre.Execute(sender as Picker);
        }

        private void PickerFilmMaker_SelectedIndexChanged(object sender, EventArgs e)
        {
             ViewModel.SelectedFilmMaker.Execute(sender as Picker); 
        }

        private void PickerActor_SelectedIndexChanged(object sender, EventArgs e)
        {
                ViewModel.SelectedActor.Execute(sender as Picker);
        }

        private void showActorPicker(object sender, EventArgs e)
        {
            actorPicker.Focus();
        }
    }
}
﻿using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.Edit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerEdit : ContentPage
	{
        //Set ViewModel for BindingContext
        private FilmMakerEditViewModel ViewModel
        {
            get
            {
                return BindingContext as FilmMakerEditViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public FilmMakerEdit (FilmMaker filmMaker)
		{
            //Setting BindingContext
            ViewModel = new FilmMakerEditViewModel(filmMaker);
			InitializeComponent ();
		}

        private void FilmMakerNameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompletedCommand.Execute(sender as Entry);
        }

        private void FilmMakerSurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompletedCommand.Execute(sender as Entry);
        }
    }
}
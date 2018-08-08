using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using System;
using System.Collections.ObjectModel;
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

        public FilmMakerEdit (FilmMaker filmmaker)
		{
            //Setting BindingContext
            ViewModel = new FilmEditViewModel(filmmaker);
			InitializeComponent ();
        }

        

        
        private void NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompletedCommand.Execute(sender as Entry);
        }
        private void SurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompletedCommand.Execute(sender as Entry);
        }

        

        

        
    }
}
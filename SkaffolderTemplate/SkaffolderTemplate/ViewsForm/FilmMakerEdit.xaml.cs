using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerEdit : ContentPage
	{
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
            ViewModel = new FilmMakerEditViewModel(filmMaker, new PageService());
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.SetPreviewsValue.Execute(null);
        }

        private void FilmMakerNameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompleted.Execute(sender as Entry);
        }

        private void FilmMakerSurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompleted.Execute(sender as Entry);
        }
    }
}
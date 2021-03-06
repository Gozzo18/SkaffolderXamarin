﻿using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkaffolderTemplate.ViewModels;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangePasswordPopUp : PopupPage
	{
        private ChangePasswordPopUpViewModel ViewModel
        {
            get
            {
                return BindingContext as ChangePasswordPopUpViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public ChangePasswordPopUp ()
		{
            ViewModel = new ChangePasswordPopUpViewModel();
			InitializeComponent ();
		}

        private void BackButton(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void SaveButton(object sender, System.EventArgs e)
        {
            ViewModel.Save.Execute(null);
        }
    }
}
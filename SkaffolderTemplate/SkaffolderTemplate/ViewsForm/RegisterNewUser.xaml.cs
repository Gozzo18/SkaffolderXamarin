﻿using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterNewUser : ContentPage
	{
        //Set ViewModel for BindingContext
        private RegisterNewUserViewModel ViewModel
        {
            get
            {
                return BindingContext as RegisterNewUserViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public RegisterNewUser ()
		{
            //Setting BindingContext
            ViewModel = new RegisterNewUserViewModel();
			InitializeComponent ();
		}

        private void RoleSelected(object sender, EventArgs e)
        {
            ViewModel.SelectedRole.Execute(sender as Picker);
        }
    }
}
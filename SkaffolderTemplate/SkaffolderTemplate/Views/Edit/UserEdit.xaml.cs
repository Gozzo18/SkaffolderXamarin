using SkaffolderTemplate.Models;
using SkaffolderTemplate.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.Edit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserEdit : ContentPage
    {
        //Set ViewModel for BindingContext
        private UserEditViewModel ViewModel
        {
            get
            {
                return BindingContext as UserEditViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public UserEdit (User user)
		{
            //Setting BindingContext
            ViewModel = new FilmEditViewModel(user);
			InitializeComponent ();
        }

        

        
        private void MailEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.MailCompletedCommand.Execute(sender as Entry);
        }
        private void NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NameCompletedCommand.Execute(sender as Entry);
        }
        private void RolesEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.RolesCompletedCommand.Execute(sender as Entry);
        }
        private void SurnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.SurnameCompletedCommand.Execute(sender as Entry);
        }
        private void UsernameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.UsernameCompletedCommand.Execute(sender as Entry);
        }

        

        

        
    }
}
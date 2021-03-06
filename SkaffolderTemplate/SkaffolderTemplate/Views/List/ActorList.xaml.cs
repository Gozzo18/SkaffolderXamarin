﻿using SkaffolderTemplate.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Views.List
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActorList : ContentPage
	{
        //Set ViewModel for BindingContext
        private ActorListViewModel ViewModel
        {
            get
            {
                return BindingContext as ActorListViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public ActorList ()
		{
            //Setting BindingContext
            ViewModel = new ActorListViewModel();
            InitializeComponent ();            
		}

        protected override void OnAppearing()
        {
            //Force garbace collector to run
            GC.Collect();
            GC.WaitForPendingFinalizers();

            base.OnAppearing();
            //Loading data with API request
            ViewModel.LoadDataCommand.Execute(null);           
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchCommand.Execute(null);
        }

        //Hide graphic effect on ListView
        private void MainListOfActors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}
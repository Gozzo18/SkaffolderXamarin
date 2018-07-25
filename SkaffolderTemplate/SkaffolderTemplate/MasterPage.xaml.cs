using SkaffolderTemplate.Models;
using SkaffolderTemplate.Support;
using SkaffolderTemplate.ViewModels;
using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
	{
        private MasterPageViewModel ViewModel
        {
            get
            {
                return BindingContext as MasterPageViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

		public MasterPage()
		{
            ViewModel = new MasterPageViewModel();
			InitializeComponent();
            Title = "MasterPage";
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
		}

        protected override void OnAppearing()
        {
            var app = App.Current as App;
            MessagingCenter.Subscribe<MasterPageViewModel, string>(this, Events.DetailPageChanged, (arg1, arg2) => {
                switch (arg2)
                {   
                    case "Actor":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ActorPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;
                
                    case "Film":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new FilmPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "FilmMaker":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new FilmMakerPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "Home":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new HomePage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "Profile":
                            loadUserData();
                            break;

                    case "Manage User":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ManageUser());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "Logout":
                            #region Delete all reference to UserLogged
                            app.AuthenticationToken = "";
                            app.UserId = "";
                            app.SavePropertiesAsync();
                            #endregion
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new LoginPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;
                }
            });
            base.OnAppearing();
        }

        private void loadUserData()
        {
          // ViewModel.GetUserById.Execute(null);
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<MasterPageViewModel, string>(this, "Detail");
            base.OnDisappearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.SetDetailPage.Execute(sender as Button);
        }
    }
}

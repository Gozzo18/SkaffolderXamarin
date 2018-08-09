using SkaffolderTemplate.Support;
using SkaffolderTemplate.ViewModels;
using SkaffolderTemplate.Views;
using SkaffolderTemplate.Views.List;
using System;
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
            MessagingCenter.Subscribe<MasterPageViewModel, string>(this, Events.DetailPageChanged, (arg1, arg2) =>
            {
                switch (arg2)
                {
                    case "Actors":
                        ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ActorsList());
                        
                        ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                        break;
                    case "Films":
                        ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new FilmsList());
                        
                        ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                        break;
                    case "FilmMakers":
                        ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new FilmMakersList());
                        
                        ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                        break;
                    case "Users":
                        
                        ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new UsersListStatic());
                        ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                        break;
                    // Start Detail Page Elements Independent
					case "H o m e":
						((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new HomePage());
						((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
						break;
					case "P r o f i l e":
						ViewModel.GetUserById.Execute(null);
						((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
						break;
					case "L o g o u t":
						#region Delete all reference to UserLogged
						Settings.AuthenticationToken = "";
						Settings.CurrentUserRole = "";
						Settings.UserId = "";
						Settings.Password = "";
						#endregion
						((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new LoginPage());
						((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
						break;
					 // End Detail Page Elements Independent
                }
            });
            base.OnAppearing();
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

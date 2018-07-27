using SkaffolderTemplate.Support;
using SkaffolderTemplate.ViewModels;
using SkaffolderTemplate.Views;
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
            MessagingCenter.Subscribe<MasterPageViewModel, string>(this, Events.DetailPageChanged, (arg1, arg2) => {
                switch (arg2)
                {   
                    case "A c t o r":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ActorPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;
                
                    case "F i l m":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new FilmPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "F i l m M a k e r":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new FilmMakerPage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "H o m e":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new HomePage());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "P r o f i l e":
                            loadUserData();
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                            break;

                    case "M a n a g e  U s e r s":
                            if (!Application.Current.Properties["CurrentUserRole"].Equals("ADMIN"))
                            {
                            //SHOW POPUP
                            return ;
                            }
                        else
                        {
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ManageUsers());
                            ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                        }

                            break;

                    case "L o g o u t":
                            #region Delete all reference to UserLogged
                            app.AuthenticationToken = "";
                            app.UserId = "";
                            app.CurrentUserRole = "";
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
          ViewModel.GetUserById.Execute(null);
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

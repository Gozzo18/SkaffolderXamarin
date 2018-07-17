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
            ViewModel = new MasterPageViewModel(new PageService());
			InitializeComponent();
            Title = "MasterPage";
            Detail = new NavigationPage(new ActorPage());
            IsPresented = false;
		}

        protected override void OnAppearing()
        {

                MessagingCenter.Subscribe<MasterPageViewModel, string>(this, "Detail", (arg1, arg2) => {
                switch (arg2)
                {
                    /*    case "Home":
                            ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage();
                            break;*/

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

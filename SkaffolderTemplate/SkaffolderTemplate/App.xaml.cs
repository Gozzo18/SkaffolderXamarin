using SkaffolderTemplate.Rest;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkaffolderTemplate.Views;
using SkaffolderTemplate.Rest.Security;
using SkaffolderTemplate.Support;
using SkaffolderTemplate.Rest.Base;
using Rg.Plugins.Popup.Services;
using SkaffolderTemplate.Extensions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SkaffolderTemplate
{
    public partial class App : Application
    {

        public static FilmRestService filmService { get; private set; }
        public static ActorRestService actorService { get; private set; }
        public static FilmMakerRestService filmMakerService { get; private set; }
        public static UserRestService userService { get; private set; }
        public static LoginRestService loginService { get; private set; }

        public App()
        {
            InitializeComponent();

            filmService = new FilmRestService();
            actorService = new ActorRestService();
            filmMakerService = new FilmMakerRestService();
            userService = new UserRestService();
            loginService = new LoginRestService();

            //Set the main page of Application
            if (string.IsNullOrEmpty(Settings.AuthenticationToken))
                MainPage = new NavigationPage(new LoginPage());
            else
                MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
            //When the app starts, it subscribe to all the rest service in order to know if the token expired
            MessagingCenter.Subscribe<ActorRestServiceBase, bool>(this, Events.TokenExpired, async (arg1, arg2) =>
            {
                await PopupNavigation.Instance.PushAsync(new TokenExpiredPopUp());
                MainPage = new NavigationPage(new LoginPage());
            });

            MessagingCenter.Subscribe<FilmRestServiceBase, bool>(this, Events.TokenExpired, async (arg1, arg2) =>
            {
                await PopupNavigation.Instance.PushAsync(new TokenExpiredPopUp());
                MainPage = new NavigationPage(new LoginPage());
            });

            MessagingCenter.Subscribe<FilmMakerRestServiceBase, bool>(this, Events.TokenExpired, async (arg1, arg2) =>
            {
                await PopupNavigation.Instance.PushAsync(new TokenExpiredPopUp());
                MainPage = new NavigationPage(new LoginPage());
            });

            MessagingCenter.Subscribe<UserRestServiceBase, bool>(this, Events.TokenExpired, async (arg1, arg2) =>
            {
                await PopupNavigation.Instance.PushAsync(new TokenExpiredPopUp());
                MainPage = new NavigationPage(new LoginPage());
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override async void OnResume()
        {
            if (!await loginService.VerifyToken(Settings.AuthenticationToken))
                MainPage = new NavigationPage(new LoginPage());
        }
    }
}

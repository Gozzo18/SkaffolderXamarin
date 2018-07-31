using SkaffolderTemplate.Rest;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkaffolderTemplate.Views;
using SkaffolderTemplate.Rest.Security;
using System;
using System.Diagnostics;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SkaffolderTemplate
{
	public partial class App : Application
	{
        public const string FILM_URL = "http://192.168.140.73:3000/api/films/";
        public const string ACTOR_URL = "http://192.168.140.73:3000/api/actors/";
        public const string FILMMAKER_URL = "http://192.168.140.73:3000/api/filmmakers/";
        public const string USER_URL = "http://192.168.140.73:3000/api/Users/";
        public const string LOGIN_URL = "http://192.168.140.73:3000/api/login";
        public const string VERIFY_TOKEN_URL = "http://192.168.140.73:3000/api/verifyToken";
        public const string CHANGE_PASSWORD_URL = "http://192.168.140.73:3000/api/changePassword";

        public static FilmRestService filmService { get; private set; }
        public static ActorRestService actorService { get; private set; }
        public static FilmMakerRestService filmMakerService { get; private set; }
        public static UserRestService userService { get; private set; }
        public static LoginRestService loginService { get; private set; }

        private const string AUTHENTICATION_TOKEN = "AuthenticationToken";
        private const string USER_ID = "UserId";
        private const string PASSWORD = "Password";
        private const string CURRENT_USER_ROLE = "CurrentUserRole";

        public App ()
		{
            InitializeComponent();

            filmService = new FilmRestService();
            actorService = new ActorRestService();
            filmMakerService = new FilmMakerRestService();
            userService = new UserRestService();
            loginService = new LoginRestService();

            //Set the main page of Application
            if (string.IsNullOrEmpty(this.Properties["AuthenticationToken"].ToString()))
                MainPage = new NavigationPage(new LoginPage());
            else
                MainPage = new MasterPage();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override async void OnResume ()
		{
            if (!await loginService.VerifyToken(this.Properties["AuthenticationToken"].ToString()))
                MainPage = new NavigationPage(new LoginPage());
        }

        public string AuthenticationToken
        {
            get
            {
                if (Current.Properties.ContainsKey(AUTHENTICATION_TOKEN))
                    return (string)Current.Properties[AUTHENTICATION_TOKEN];
                return "";
            }
            set
            {
                Current.Properties[AUTHENTICATION_TOKEN] = value;
            }
        }

        public string UserId
        {
            get
            {
                if (Current.Properties.ContainsKey(USER_ID))
                    return (string)Current.Properties[USER_ID];
                return "";
            }
            set
            {
                Current.Properties[USER_ID] = value;
            }
        }

        public string Password
        {
            get
            {
                if (Current.Properties.ContainsKey(PASSWORD))
                    return (string)Current.Properties[PASSWORD];
                return "";
            }
            set
            {
                Current.Properties[PASSWORD] = value;
            }
        }

        public string CurrentUserRole
        {
            get
            {
                if (Current.Properties.ContainsKey(CURRENT_USER_ROLE))
                    return (string)Current.Properties[CURRENT_USER_ROLE];
                return "";
            }
            set
            {
                Current.Properties[CURRENT_USER_ROLE] = value;
            }
        }
    }
}

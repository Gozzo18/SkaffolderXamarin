using SkaffolderTemplate.Rest;
using SkaffolderTemplate.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SkaffolderTemplate
{
	public partial class App : Application
	{
       // casa
       // public const string FILM_URL = "http://192.168.1.88:3000/api/films/";
       // public const string ACTOR_URL = "http://192.168.1.88:3000/api/actors/";
       // public const string FILMMAKER_URL = "http://192.168.1.88:3000/api/filmmakers/";

        //lavoro
        public const string FILM_URL = "http://192.168.140.73:3000/api/films/";
        public const string ACTOR_URL = "http://192.168.140.73:3000/api/actors/";
        public const string FILMMAKER_URL = "http://192.168.140.73:3000/api/filmmakers/";

        public static FilmRestService filmService { get; private set; }
        public static ActorRestService actorService { get; private set; }
        public static FilmMakerRestService filmMakerService { get; private set; }

		public App ()
		{
            InitializeComponent();

            filmService = new FilmRestService();
            actorService = new ActorRestService();
            filmMakerService = new FilmMakerRestService();

			MainPage = new NavigationPage(new FilmPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

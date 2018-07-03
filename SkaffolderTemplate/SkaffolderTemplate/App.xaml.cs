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
        //casa
        //public const string FILM_URL = "http://192.168.1.20:3000/api/films/";

        //lavoro
        public const string FILM_URL = "http://192.168.140.73:3000/api/films/";

        public static FilmManager fm { get; private set; }

		public App ()
		{
			InitializeComponent();

            fm = new FilmManager(new FilmRestService());
			MainPage = new FilmPage();
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

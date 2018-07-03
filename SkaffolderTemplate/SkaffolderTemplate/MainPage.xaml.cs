using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkaffolderTemplate
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        async void vaiAvanti(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FilmPage(), false);
        }
    }
}

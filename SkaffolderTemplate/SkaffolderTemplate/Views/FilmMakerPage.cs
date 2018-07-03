using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SkaffolderTemplate.Views
{
	public class FilmMakerPage : ContentPage
	{
		public FilmMakerPage ()
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}
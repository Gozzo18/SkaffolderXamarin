using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkaffolderTemplate.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.ViewsForm
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilmMakerEdit : ContentPage
	{
		public FilmMakerEdit (FilmMaker filmMakerPassato)
		{
			InitializeComponent ();
		}
	}
}
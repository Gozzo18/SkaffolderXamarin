using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkaffolderTemplate.Extensions
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FakePopUp : ContentView
	{
		public FakePopUp ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.IsVisible = false;
        }
    }
}
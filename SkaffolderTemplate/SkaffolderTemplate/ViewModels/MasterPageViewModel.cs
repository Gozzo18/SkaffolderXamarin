using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {

        public ICommand SetDetailPage { get; private set; }

        public MasterPageViewModel()
        {
            SetDetailPage = new Command<Button>(vm => UpdateDetailPage(vm));
        }

        private void UpdateDetailPage(Button button)
        {
            MessagingCenter.Send<MasterPageViewModel, string>(this, "Detail", button.Text);
        }

    }
}

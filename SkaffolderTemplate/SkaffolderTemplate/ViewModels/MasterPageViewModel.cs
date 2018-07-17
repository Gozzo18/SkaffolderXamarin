using SkaffolderTemplate.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;

        public ICommand SetDetailPage { get; private set; }

        public MasterPageViewModel(IPageService PageService)
        {
            _pageService = PageService;
            SetDetailPage = new Command<Button>(vm => UpdateDetailPage(vm));
        }

        private void UpdateDetailPage(Button button)
        {
            MessagingCenter.Send<MasterPageViewModel, string>(this, "Detail", button.Text);
        }

    }
}

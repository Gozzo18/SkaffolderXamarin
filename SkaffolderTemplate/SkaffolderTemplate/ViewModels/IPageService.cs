using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkaffolderTemplate.ViewModels
{
    public interface IPageService
    {
        Task PushAsync(Page page, bool animation);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    }
}

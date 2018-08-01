using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SkaffolderTemplate.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //We set here the handling of a change in an attributes
        protected void SetValue<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
                return;
            oldValue = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}

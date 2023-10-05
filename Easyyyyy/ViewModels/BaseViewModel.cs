using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Easyyyyy.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged([CallerMemberName]string Property = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }
    }
}

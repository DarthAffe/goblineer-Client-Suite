using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WyrezPriceClient.Misc
{
    public class AbstractBindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

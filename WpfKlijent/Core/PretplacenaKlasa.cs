using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfKlijent.Core
{
    public class PretplacenaKlasa<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void pretplatiSeNaPromenu<T>(ref T polje, T value, [CallerMemberName] string propertyName = "")
        {
            polje = value;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

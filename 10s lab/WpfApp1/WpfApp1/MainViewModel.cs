using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace WpfApp1
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<StorageItemViewModel> ItemLists { get; set; }

        public MainViewModel(IEnumerable<StorageItem> itemStor)
        {
            ItemLists = new ObservableCollection<StorageItemViewModel>(itemStor.Select(b => new StorageItemViewModel(b)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

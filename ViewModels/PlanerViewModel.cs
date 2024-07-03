using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Planer.DataModel;

namespace Planer.ViewModels
{
    public class PlanerViewModel : ViewModelBase
    {
        public ObservableCollection<PlanerItem> ListItems { get; }

        public PlanerViewModel(IEnumerable<PlanerItem> items) : base()
        {
            ListItems = new ObservableCollection<PlanerItem>(items);
            foreach (var item in ListItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
            ListItems.CollectionChanged += ListItems_CollectionChanged;
        }

        private void ListItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PlanerItem newItem in e.NewItems)
                {
                    newItem.PropertyChanged += Item_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (PlanerItem oldItem in e.OldItems)
                {
                    oldItem.PropertyChanged -= Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                var item = sender as PlanerItem;
                if (item != null && item.IsChecked)
                {
                    ListItems.Remove(item);
                }
            }
        }
    }
}

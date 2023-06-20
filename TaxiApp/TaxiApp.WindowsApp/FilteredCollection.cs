using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace TaxiApp.WindowsApp
{
    internal sealed class FilteredCollection<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        private IEnumerable<T> _items;
        private Func<T, bool> _filter;

        public FilteredCollection()
        {
        }

        public FilteredCollection(IEnumerable<T> items)
        {
            _items = items;
        }

        public IEnumerable<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                UpdateCollection();
            }
        }

        public Func<T, bool> Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                UpdateCollection();
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public IEnumerator<T> GetEnumerator()
        {
            var items = _items;

            if (Filter != null)
                items = items.Where(Filter);

            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void UpdateCollection()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

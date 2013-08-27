using System;

namespace NWS {
    public class ItemEventArgs<T> : EventArgs {
        public T Item { get; private set; }
        public ItemEventArgs(T item) {
            Item = item;
        }
    }
}
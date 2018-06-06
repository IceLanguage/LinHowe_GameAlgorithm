using System.Collections.Generic;

namespace TsiU
{
    public class TBlackBoard
    {
        class TBlackboardItem
        {
            private object _value;
            public void SetValue(object v)
            {
                _value = v;
            }
            public T GetValue<T>()
            {
                return (T)_value;
            }
        }
        private Dictionary<string, TBlackboardItem> _items;

        public TBlackBoard()
        {
            _items = new Dictionary<string, TBlackboardItem>();
        }
        public void SetValue(string key, object v)
        {
            TBlackboardItem item;
            if (_items.ContainsKey(key) == false)
            {
                item = new TBlackboardItem();
                _items.Add(key, item);
            }
            else
            {
                item = _items[key];
            }
            item.SetValue(v);
        }
        public T GetValue<T>(string key, T defaultValue)
        {
            if (_items.ContainsKey(key) == false)
            {
                return defaultValue;
            }
            return _items[key].GetValue<T>();
        }
    }
}

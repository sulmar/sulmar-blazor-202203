using System.ComponentModel;
using System.Globalization;

namespace Shopper.BlazorWebAssembly
{
    public class InMemoryStorageProvider : IStorageProvider
    {
        private IDictionary<string, string> storage = new Dictionary<string, string>();

        public string GetItem(string key)
        {
            return storage.ContainsKey(key) ? storage[key] : default;
        }

        public T GetItem<T>(string key)
        {
            return TryParse<T>(GetItem(key));
        }

        private static T TryParse<T>(string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
        }

        public void SetItem(string key, string value)
        {            
            if (storage.ContainsKey(key))
            {
                storage[key] = value;
            }
            else
            {
                storage.Add(key, value);
            }
        }

        public void SetItem<T>(string key, T value)
        {
            SetItem(key, value.ToString());
        }
    }
}

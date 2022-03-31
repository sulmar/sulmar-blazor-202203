using Microsoft.JSInterop;
using System.ComponentModel;
using System.Globalization;

namespace Shopper.BlazorWebAssembly
{

    // Inspired by https://github.com/Blazored/LocalStorage
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly IJSInProcessRuntime js;
        public LocalStorageProvider(IJSInProcessRuntime js)
        {
            this.js = js;
        }

        public string GetItem(string key)
        {
            return js.Invoke<string>("localStorage.getItem", key);
        }

        public T GetItem<T>(string key)
        {            
            return TryParse<T>(GetItem(key));
        }

        private static T TryParse<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            else
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
            }
        }

        public void SetItem(string key, string value)
        {
            js.InvokeVoid("localStorage.setItem", key, value);
        }

        public void SetItem<T>(string key, T value)
        {
            SetItem(key, value.ToString());
        }
    }
}

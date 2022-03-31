using Microsoft.JSInterop;
using System.ComponentModel;
using System.Globalization;

namespace Shopper.BlazorWebAssembly
{
    public interface IStorageProvider
    {
        string GetItem(string key);
        void SetItem(string key, string value);
        void SetItem<T>(string key, T value);
        T GetItem<T>(string key);
    }

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
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            return (T) converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
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


    public class StateContainer
    {
        private int currentCount;

        public int CurrentCount
        {
            get { return currentCount; }
            set 
            { 
                currentCount = value; 


            
            }
        }
    }
}

namespace Shopper.BlazorWebAssembly
{
    public interface IStorageProvider
    {
        string GetItem(string key);
        void SetItem(string key, string value);
        void SetItem<T>(string key, T value);
        T GetItem<T>(string key);
    }


    public class StateContainer
    {
        private readonly IStorageProvider storageProvider;

        public StateContainer(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;

            currentCount = storageProvider.GetItem<int>(nameof(CurrentCount));
        }

        private int currentCount;

        public int CurrentCount
        {
            get { return currentCount; }
            set 
            { 
                currentCount = value;

                storageProvider.SetItem(nameof(CurrentCount), currentCount);

                OnChange?.Invoke();
            }
        }

        public event Action? OnChange;


    }
}

namespace Core.Storage.Contracts
{
    public interface IStorageProvider
    {
        void Save(object data, string id);
        T Load<T>(string id) where T : class;
    }
}

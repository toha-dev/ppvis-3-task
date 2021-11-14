using Core.Models;
using Core.Storage.Contracts;

namespace Core.Storage
{
    public class DummyStorageProvider : IStorageProvider
    {
        public readonly IDiseasesStorageProvider _provider;

        public DummyStorageProvider(IDiseasesStorageProvider provider)
        {
            _provider = provider;
        }

        public void Save(object data, string id)
        {
            
        }

        public T Load<T>(string id) where T : class
        {
            User user;

            switch (id)
            {
                case "1":
                {
                    user = new User(_provider.GetDisease("0"), id);
                    break;
                }
                default:
                {
                    user = new User(null, id);
                    break;
                }
            }

            return user as T;
        }
    }
}

using Core.Models;

namespace Core.Storage.Contracts
{
    public interface IDiseasesStorageProvider
    {
        Disease GetDisease(string id);
    }
}

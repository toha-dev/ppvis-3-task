using System.Collections.Generic;

namespace Core.Storage.Contracts
{
    public interface IDiseasesHistoryStorageProvider
    {
        void AddDisease(string userId, string diseaseId);
        IEnumerable<string> GetDiseasesHistory(string userId);
    }
}

using System.Collections.Generic;

namespace Core.Storage.Contracts
{
    public interface IMedicamentsHistoryStorageProvider
    {
        void AddMedicament(string userId, string medicamentId);
        IEnumerable<string> GetUserPreviousMedicamentsId(string userId);
    }
}

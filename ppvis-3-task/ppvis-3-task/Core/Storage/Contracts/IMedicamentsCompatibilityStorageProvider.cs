using System.Collections.Generic;

namespace Core.Storage.Contracts
{
    public interface IMedicamentsCompatibilityStorageProvider
    {
        bool IsCompatible(IEnumerable<string> previousMedicamentsId, string targetMedicamentId);
    }
}

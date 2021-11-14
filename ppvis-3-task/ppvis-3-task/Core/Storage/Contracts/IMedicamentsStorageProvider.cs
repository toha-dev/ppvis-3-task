using Core.Models;
using System.Collections.Generic;

namespace Core.Storage.Contracts
{
    public interface IMedicamentsStorageProvider
    {
        IEnumerable<string> AllMedicaments { get; }

        IEnumerable<string> GetMedicamentsIdForDisease(string diseaseId);
        Medicament GetMedicament(string id);

        bool IsMedicamentAvailable(string id);
        void RegisterMedicament(string id);
    }
}

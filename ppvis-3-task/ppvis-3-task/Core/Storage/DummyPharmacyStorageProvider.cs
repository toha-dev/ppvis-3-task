using Core.Storage.Contracts;
using System.Collections.Generic;

namespace Core.Storage
{
    public class DummyPharmacyStorageProvider : IPharmacyStorageProvider
    {
        private readonly Dictionary<string, bool> _medicamentsStatus;

        public DummyPharmacyStorageProvider(Dictionary<string, bool> medicamentsStatus)
        {
            _medicamentsStatus = medicamentsStatus;
        }

        public bool IsInStock(string medicamentId)
        {
            return _medicamentsStatus.ContainsKey(medicamentId) && _medicamentsStatus[medicamentId];
        }
    }
}

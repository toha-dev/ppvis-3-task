using Core.Models;
using Core.Storage.Contracts;
using System.Collections.Generic;

namespace Core.Storage
{
    public class SmartestMedicamentsStorageProvider : SmarterMedicamentsStorageProvider, IMedicamentsStorageProvider
    {
        private readonly IPharmacyStorageProvider _pharmacyProvider;

        public SmartestMedicamentsStorageProvider(IEnumerable<Medicament> available, IEnumerable<Medicament> notAvailable,
            Dictionary<string, IEnumerable<string>> replacers, IMedicamentsHistoryStorageProvider historyProvider, IMedicamentsCompatibilityStorageProvider compatibilityProvider, 
            IPharmacyStorageProvider pharmacyProvider) : base(available, notAvailable, replacers, historyProvider, compatibilityProvider)
        {
            _pharmacyProvider = pharmacyProvider;
        }

        protected override bool ShouldReplace(string sourceId, string targetId)
        {
            return base.ShouldReplace(sourceId, targetId) || _pharmacyProvider.IsInStock(targetId);
        }
    }
}

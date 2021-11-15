using Core.Models;
using Core.Storage.Contracts;
using System.Collections.Generic;

namespace Core.Storage
{
    public class SmarterMedicamentsStorageProvider : SmartMedicamentsStorageProvider, IMedicamentsStorageProvider
    {
        private readonly IMedicamentsHistoryStorageProvider _historyProvider;
        private readonly IMedicamentsCompatibilityStorageProvider _compatibilityProvider;

        public SmarterMedicamentsStorageProvider(IEnumerable<Medicament> available, IEnumerable<Medicament> notAvailable, 
            Dictionary<string, IEnumerable<string>> replacers, IMedicamentsHistoryStorageProvider historyProvider, IMedicamentsCompatibilityStorageProvider compatibilityProvider) : base(available, notAvailable, replacers)
        {
            _historyProvider = historyProvider;
            _compatibilityProvider = compatibilityProvider;
        }

        protected override bool ShouldReplace(string sourceId, string targetId)
        {
            return base.ShouldReplace(sourceId, targetId) && _compatibilityProvider.IsCompatible(_historyProvider.GetUserPreviousMedicamentsId(User.Instance.Id), targetId);
        }
    }
}

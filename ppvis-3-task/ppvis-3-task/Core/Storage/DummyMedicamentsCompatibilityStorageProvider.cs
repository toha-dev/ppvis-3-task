using Core.Storage.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Core.Storage
{
    public class DummyMedicamentsCompatibilityStorageProvider : IMedicamentsCompatibilityStorageProvider
    {
        private readonly Dictionary<string, IEnumerable<string>> _notCompatible;

        public DummyMedicamentsCompatibilityStorageProvider(Dictionary<string, IEnumerable<string>> notCompatible)
        {
            _notCompatible = notCompatible;
        }

        public bool IsCompatible(IEnumerable<string> previousMedicamentsId, string targetMedicamentId)
        {
            foreach (var previousMedicamentId in previousMedicamentsId)
            {
                if (_notCompatible.ContainsKey(previousMedicamentId))
                {
                    if (_notCompatible[previousMedicamentId].Contains(targetMedicamentId))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

using Core.Models;
using Core.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Storage
{
    public class SmartMedicamentsStorageProvider : DummyMedicamentsStorageProvider, IMedicamentsStorageProvider, IMedicamentsUnregisterService
    {
        protected readonly Dictionary<string, IEnumerable<string>> Replacers;

        public SmartMedicamentsStorageProvider(IEnumerable<Medicament> available, IEnumerable<Medicament> notAvailable, Dictionary<string, IEnumerable<string>> replacers) : base(available, notAvailable)
        {
            Replacers = replacers;
        }

        public new IEnumerable<string> GetMedicamentsIdForDisease(string diseaseId)
        {
            var medicamentsId = base.GetMedicamentsIdForDisease(diseaseId).ToList();

            for (int i = 0; i < medicamentsId.Count; ++i)
            {
                if (IsMedicamentAvailable(medicamentsId[i]) == false)
                {
                    if (TryReplaceMedicament(medicamentsId[i], out var replaceId))
                    {
                        medicamentsId[i] = replaceId;
                    }
                }
            }

            return medicamentsId;
        }

        protected bool TryReplaceMedicament(string sourceId, out string resultId)
        {
            if (Replacers.ContainsKey(sourceId))
            {
                var replacers = Replacers[sourceId];

                foreach (var replacerId in replacers)
                {
                    if (ShouldReplace(sourceId, replacerId))
                    {
                        resultId = replacerId;
                        return true;
                    }
                }
            }

            resultId = null;
            return false;
        }

        protected virtual bool ShouldReplace(string sourceId, string targetId) => IsMedicamentAvailable(targetId);

        public void UnregisterMedicament(string id)
        {
            if (Available.ContainsKey(id))
            {
                NotAvailable.Add(id, Available[id]);
                Available.Remove(id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("There is no such unavailable medicament");
            }
        }
    }
}

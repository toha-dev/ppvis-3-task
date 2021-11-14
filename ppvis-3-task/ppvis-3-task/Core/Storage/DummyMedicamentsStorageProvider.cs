using Core.Models;
using Core.Storage.Contracts;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Core.Storage
{
    public class DummyMedicamentsStorageProvider : IMedicamentsStorageProvider
    {
        protected readonly Dictionary<string, Medicament> Available = new Dictionary<string, Medicament>();
        protected readonly Dictionary<string, Medicament> NotAvailable = new Dictionary<string, Medicament>();

        public IEnumerable<string> AllMedicaments => Available.Keys.Concat(NotAvailable.Keys);

        public DummyMedicamentsStorageProvider(IEnumerable<Medicament> available, IEnumerable<Medicament> notAvailable)
        {
            foreach (var x in available)
            {
                Available.Add(x.Id, x);
            }

            foreach (var x in notAvailable)
            {
                NotAvailable.Add(x.Id, x);
            }
        }

        public IEnumerable<string> GetMedicamentsIdForDisease(string diseaseId)
        {
            switch (diseaseId)
            {
                case "0": return new[] { "0", "2", "4" };
                case "1": return new[] { "1", "3", "5" };
                case "2": return new[] { "1", "3", };
                case "3": return new[] { "2", "3", "4", "5" };
            }

            return new[] { "1", "2", "3" };
        }

        public Medicament GetMedicament(string id)
        {
            if (Available.ContainsKey(id))
            {
                return Available[id];
            }

            if (NotAvailable.ContainsKey(id))
            {
                return NotAvailable[id];
            }

            throw new ArgumentOutOfRangeException("There is no such medicament");
        }

        public bool IsMedicamentAvailable(string id)
        {
            return Available.ContainsKey(id);
        }

        public void RegisterMedicament(string id)
        {
            if (NotAvailable.ContainsKey(id))
            {
                Available.Add(id, NotAvailable[id]);
                NotAvailable.Remove(id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("There is no such medicament");
            }
        }
    }
}

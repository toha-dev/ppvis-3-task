using Core.Storage.Contracts;
using System.Collections.Generic;

namespace Core.Storage
{
    public class DummyMedicamentsHistoryStorageProvider : IMedicamentsHistoryStorageProvider
    {
        private readonly Dictionary<string, List<string>> _used;

        public DummyMedicamentsHistoryStorageProvider(Dictionary<string, List<string>> used)
        {
            _used = used;
        }

        public void AddMedicament(string userId, string medicamentId)
        {
            if (_used.ContainsKey(userId) == false)
            {
                _used.Add(userId, new List<string>());
            }

            _used[userId].Add(medicamentId);
        }

        public IEnumerable<string> GetUserPreviousMedicamentsId(string userId)
        {
            if (_used.ContainsKey(userId))
            {
                return _used[userId];
            }

            return new List<string>();
        }
    }
}

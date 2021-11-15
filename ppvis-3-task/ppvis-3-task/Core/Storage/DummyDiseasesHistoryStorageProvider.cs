using Core.Storage.Contracts;
using System.Collections.Generic;

namespace Core.Storage
{
    public class DummyDiseasesHistoryStorageProvider : IDiseasesHistoryStorageProvider
    {
        private readonly Dictionary<string, List<string>> _usersHistory = new Dictionary<string, List<string>>();

        public void AddDisease(string userId, string diseaseId)
        {
            if (_usersHistory.ContainsKey(userId) == false)
            {
                _usersHistory.Add(userId, new List<string>());
            }

            _usersHistory[userId].Add(diseaseId);
        }

        public IEnumerable<string> GetDiseasesHistory(string userId)
        {
            if (userId == "1")
            {
                return new[] { "0", "1" };
            }

            if (_usersHistory.ContainsKey(userId))
            {
                return _usersHistory[userId];
            }

            return new List<string>();
        }
    }
}

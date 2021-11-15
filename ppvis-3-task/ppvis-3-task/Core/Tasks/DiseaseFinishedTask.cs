using Core.Models;
using Core.Storage.Contracts;
using Core.Tasks.Contracts;

namespace Core.Tasks
{
    public class DiseaseFinishedTask : ITaskHandler
    {
        private readonly IMedicamentsStorageProvider _medicamentsProvider;
        private readonly IMedicamentsHistoryStorageProvider _historyProvider;

        public DiseaseFinishedTask(IMedicamentsStorageProvider medicamentsProvider, IMedicamentsHistoryStorageProvider historyProvider)
        {
            _medicamentsProvider = medicamentsProvider;
            _historyProvider = historyProvider;
        }

        public void Execute()
        {
            var userId = User.Instance.Id;
            var diseaseId = User.Instance.CurrentDisease.Id;

            var usedMedicaments = _medicamentsProvider.GetMedicamentsIdForDisease(diseaseId);

            foreach (var medicamentId in usedMedicaments)
            {
                _historyProvider.AddMedicament(userId, medicamentId);
            }

            User.Instance.CurrentDisease = null;
        }
    }
}

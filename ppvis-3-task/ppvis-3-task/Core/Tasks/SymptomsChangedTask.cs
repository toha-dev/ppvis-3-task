using Core.Models;
using Core.Storage.Contracts;
using Core.Tasks.Contracts;

namespace Core.Tasks
{
    public class SymptomsChangedTask : ITaskHandler
    {
        private readonly ITaskHandler _testTask;
        private readonly ITaskHandler _showMedicamentsTask;
        private readonly IDiseasesHistoryStorageProvider _historyProvider;

        public SymptomsChangedTask(ITaskHandler testTask, ITaskHandler showMedicamentsTask, IDiseasesHistoryStorageProvider historyProvider)
        {
            _testTask = testTask;
            _showMedicamentsTask = showMedicamentsTask;
            _historyProvider = historyProvider;
        }

        public void Execute()
        {
            var oldDiseaseId = User.Instance.CurrentDisease.Id;

            _testTask.Execute();

            if (User.Instance.CurrentDisease != null)
            {
                if (User.Instance.CurrentDisease.Id != oldDiseaseId)
                {
                    _historyProvider.AddDisease(User.Instance.Id, oldDiseaseId);
                }

                _showMedicamentsTask.Execute();
            }
        }
    }
}

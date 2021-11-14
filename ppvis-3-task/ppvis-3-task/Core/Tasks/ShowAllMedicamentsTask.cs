using Core.Storage.Contracts;
using Core.Tasks.Contracts;
using System;

namespace Core.Tasks
{
    public class ShowAllMedicamentsTask : ITaskHandler
    {
        private readonly IMedicamentsStorageProvider _provider;

        public ShowAllMedicamentsTask(IMedicamentsStorageProvider provider)
        {
            _provider = provider;
        }

        public void Execute()
        {
            var medicamentsId = _provider.AllMedicaments;

            foreach (var id in medicamentsId)
            {
                var medicament = _provider.GetMedicament(id);
                var available = _provider.IsMedicamentAvailable(id);

                var prefix = available ? "Available" : "Not available";

                Console.WriteLine($"{prefix} medicament {medicament.Name}");
            }
        }
    }
}

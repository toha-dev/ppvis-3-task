using Core.Models;
using Core.Storage.Contracts;
using Core.Tasks.Contracts;
using System;

namespace Core.Tasks
{
    public class ShowMedicamentsTask : ITaskHandler
    {
        private readonly IMedicamentsStorageProvider _provider;

        public ShowMedicamentsTask(IMedicamentsStorageProvider provider)
        {
            _provider = provider;
        }

        public void Execute()
        {
            var medicaments = _provider.GetMedicamentsIdForDisease(User.Instance.CurrentDisease.Id);

            Console.WriteLine("Medicaments: ");
            foreach (var id in medicaments)
            {
                var medicament = _provider.GetMedicament(id);

                if (_provider.IsMedicamentAvailable(id))
                {
                    Console.WriteLine($"  Available {medicament.Name} use {medicament.FrequencyPerDay} " +
                        $"times per day {medicament.Days} days in row");
                }
                else
                {
                    Console.WriteLine($"  Not available {medicament.Name} [id: {medicament.Id}]");
                }
            }
        }
    }
}

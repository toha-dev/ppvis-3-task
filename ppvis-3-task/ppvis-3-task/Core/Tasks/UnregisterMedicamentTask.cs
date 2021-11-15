using Core.Storage.Contracts;
using Core.Tasks.Contracts;
using System;

namespace Core.Tasks
{
    public class UnregisterMedicamentTask : ITaskHandler
    {
        private readonly IMedicamentsUnregisterService _service;

        public UnregisterMedicamentTask(IMedicamentsUnregisterService service)
        {
            _service = service;
        }

        public void Execute()
        {
            Console.Write("Enter medicament id: ");
            _service.UnregisterMedicament(Console.ReadLine());
        }
    }
}

using Core.Storage.Contracts;
using Core.Tasks.Contracts;
using System;

namespace Core.Tasks
{
    public class RegisterMedicamentTask : ITaskHandler
    {
        private readonly IMedicamentsStorageProvider _provider;

        public RegisterMedicamentTask(IMedicamentsStorageProvider provider)
        {
            _provider = provider;
        }

        public void Execute()
        {
            Console.Write("Enter medicament id: ");
            _provider.RegisterMedicament(Console.ReadLine());
        }
    }
}

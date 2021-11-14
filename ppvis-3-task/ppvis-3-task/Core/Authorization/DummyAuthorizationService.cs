using Core.Authorization.Contracts;
using Core.Models;
using Core.Storage.Contracts;
using System;

namespace Core.Authorization
{
    public class DummyAuthorizationService : IAuthorizationService
    {
        private readonly IStorageProvider _provider;

        public DummyAuthorizationService(IStorageProvider provider)
        {
            _provider = provider;
        }

        public void Authorize()
        {
            Console.Write("Enter your id: ");
            string id = Console.ReadLine();

            _provider.Load<User>(id);
        }
    }
}

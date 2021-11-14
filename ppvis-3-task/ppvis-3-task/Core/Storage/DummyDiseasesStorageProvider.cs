using Core.Models;
using Core.Storage.Contracts;
using System;
using System.Collections.Generic;

namespace Core.Storage
{
    public class DummyDiseasesStorageProvider : IDiseasesStorageProvider
    {
        private readonly Dictionary<string, Disease> _diseases;
        
        public DummyDiseasesStorageProvider(Dictionary<string, Disease> diseases)
        {
            _diseases = diseases;
        }

        public Disease GetDisease(string id)
        {
            if (_diseases.ContainsKey(id))
            {
                return _diseases[id];
            }

            throw new ArgumentOutOfRangeException("There is no such key");
        }
    }
}

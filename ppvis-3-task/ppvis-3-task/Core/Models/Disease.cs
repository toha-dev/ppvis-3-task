using Core.Testing.Questions.Validators.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public class Disease
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<string> SymptomsId { get; }

        private readonly Dictionary<string, ISymptomValidator> _validators;

        public Disease(string id, string name, Dictionary<string, ISymptomValidator> validators)
        {
            Id = id;
            Name = name;

            SymptomsId = validators.Keys;
            _validators = validators;
        }

        public bool IsPossible()
        {
            var validators = _validators.Values;
            return validators.Any(x => x.Validate());
        }
    }
}

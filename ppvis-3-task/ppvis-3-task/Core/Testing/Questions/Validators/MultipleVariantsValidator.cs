using Core.Testing.Questions.Validators.Contracts;
using Questions.Providers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Core.Testing.Questions.Validators
{
    public class MultipleVariantsValidator : ISymptomValidator
    {
        private readonly IEnumerable<int> _correct;
        private readonly IMultipleVariantsProvider _provider;

        public MultipleVariantsValidator(IEnumerable<int> correct, IMultipleVariantsProvider provider)
        {
            _correct = correct;
            _provider = provider;
        }

        public bool Validate()
        {
            var answers = _provider.Value;

            return answers.Count() == _correct.Count()
                && _correct.All(x => answers.Contains(x));
        }
    }
}

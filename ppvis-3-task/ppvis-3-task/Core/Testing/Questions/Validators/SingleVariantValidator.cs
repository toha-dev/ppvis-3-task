using Core.Testing.Providers.Contracts;
using Core.Testing.Questions.Validators.Contracts;

namespace Core.Testing.Questions.Validators
{
    public class SingleVariantValidator : ISymptomValidator
    {
        private readonly int _correct;

        private readonly ISingleVariantProvider _provider;

        public SingleVariantValidator(int correct, ISingleVariantProvider provider)
        {
            _correct = correct;
            _provider = provider;
        }

        public bool Validate()
        {
            return _provider.Value == _correct;
        }
    }
}

using Core.Testing.Providers.Contracts;
using Core.Testing.Questions.Validators.Contracts;

namespace Core.Testing.Questions.Validators
{
    public class RangeNumericValidator : ISymptomValidator
    {
        private readonly float _min;
        private readonly float _max;

        private readonly INumericResultProvider _provider;

        public RangeNumericValidator(float min, float max, INumericResultProvider provider)
        {
            _min = min;
            _max = max;
            _provider = provider;
        }

        public bool Validate()
        {
            return _provider.Value >= _min && _provider.Value <= _max;
        }
    }
}

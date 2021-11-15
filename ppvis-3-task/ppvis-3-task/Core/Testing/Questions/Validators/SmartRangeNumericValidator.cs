using Core.Models;
using Core.Storage.Contracts;
using Core.Testing.Providers.Contracts;
using Core.Testing.Questions.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Testing.Questions.Validators
{
    public class SmartRangeNumericValidator : RangeNumericValidator, ISymptomValidator
    {
        private readonly IEnumerable<string> _afterPreviousDesiases;
        private readonly IDiseasesHistoryStorageProvider _historyProvider;

        public SmartRangeNumericValidator(float min, float max, INumericResultProvider provider,
            IEnumerable<string> afterPreviousDesiases, IDiseasesHistoryStorageProvider historyProvider) : base(min, max, provider)
        {
            _afterPreviousDesiases = afterPreviousDesiases;
            _historyProvider = historyProvider;
        }

        public new bool Validate()
        {
            var previousUserDiseases = _historyProvider.GetDiseasesHistory(User.Instance.Id);

            foreach (var diseaseId in previousUserDiseases)
            {
                if (_afterPreviousDesiases.Contains(diseaseId))
                {
                    return true;
                }
            }

            return base.Validate();
        }
    }
}

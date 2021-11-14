using System.Collections.Generic;

namespace Questions.Providers.Contracts
{
    public interface IMultipleVariantsProvider
    {
        IEnumerable<int> Value { get; }
    }
}

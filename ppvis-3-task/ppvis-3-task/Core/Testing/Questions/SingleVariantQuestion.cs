using Core.Testing.Providers.Contracts;
using System;

namespace Core.Testing.Questions
{
    public class SingleVariantQuestion : QuestionBase, ISingleVariantProvider
    {
        public int Value { get; private set; }

        public SingleVariantQuestion(string description) : base(description)
        {

        }

        public override void Ask()
        {
            Console.Write($"  {Description}: ");
            Value = int.Parse(Console.ReadLine());
        }
    }
}

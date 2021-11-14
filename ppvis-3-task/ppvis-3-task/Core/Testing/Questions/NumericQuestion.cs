using Core.Testing.Providers.Contracts;
using System;

namespace Core.Testing.Questions
{
    public class NumericQuestion : QuestionBase, INumericResultProvider
    {
        public float Value { get; private set; }

        public NumericQuestion(string description) : base(description)
        {

        }

        public override void Ask()
        {
            Console.Write($"  {Description}: ");
            Value = float.Parse(Console.ReadLine());
        }
    }
}

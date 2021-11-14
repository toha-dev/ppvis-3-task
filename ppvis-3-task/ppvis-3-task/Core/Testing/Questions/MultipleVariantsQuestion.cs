using Questions.Providers.Contracts;
using System;
using System.Collections.Generic;

namespace Core.Testing.Questions
{
    public class MultipleVariantsQuestion : QuestionBase, IMultipleVariantsProvider
    {
        public IEnumerable<int> Value { get; private set; }

        public MultipleVariantsQuestion(string description) : base(description)
        {

        }

        public override void Ask()
        {
            var answers = new List<int>();

            Console.WriteLine($"  {Description} (Enter -1 to submit question): ");

            int answer = 0;
            do
            {
                Console.Write("  Enter: ");
                answer = int.Parse(Console.ReadLine());

                if (answer != -1)
                {
                    answers.Add(answer);
                }
            } while (answer != -1);

            Value = answers;
        }
    }
}

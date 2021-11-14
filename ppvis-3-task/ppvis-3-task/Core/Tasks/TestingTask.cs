using Core.Models;
using Core.Tasks.Contracts;
using Core.Testing;
using System;
using System.Linq;

namespace Core.Tasks
{
    public class TestingTask : ITaskHandler
    {
        private readonly Test _test;

        public TestingTask(Test test)
        {
            _test = test;
        }

        public void Execute()
        {
            Console.WriteLine("Test started");

            var result = _test.Run();

            Console.WriteLine("Test ended");

            int index = 0;

            if (result.Count() > 1)
            {
                for (int i = 0; i < result.Count(); ++i)
                {
                    Console.WriteLine($"  {i}. {result.ElementAt(i).Name}");
                }

                Console.Write("Choose disease to treat: ");
                index = int.Parse(Console.ReadLine());
            }
            else if (result.Any() == false)
            {
                User.Instance.CurrentDisease = null;
                Console.WriteLine("No diseases to treat.");
                return;
            }

            User.Instance.CurrentDisease = result.ElementAt(index);
            Console.WriteLine($"  You choose: {result.ElementAt(index).Name}");
        }
    }
}

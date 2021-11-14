using Core.Tasks.Contracts;
using System;
using System.Collections.Generic;

namespace Core.Tasks
{
    public class MenuTask : ITaskHandler
    {
        public readonly Dictionary<string, ITaskHandler> _items;

        public MenuTask(Dictionary<string, ITaskHandler> items)
        {
            _items = items;
        }

        public void Execute()
        {
            while (true)
            {
                Console.WriteLine("Menu: ");

                foreach (var item in _items.Keys)
                {
                    Console.WriteLine($"  {item}");
                }

                Console.Write("Enter: ");
                string key = Console.ReadLine();

                if (_items.ContainsKey(key))
                {
                    _items[key].Execute();
                }
            }
        }
    }
}

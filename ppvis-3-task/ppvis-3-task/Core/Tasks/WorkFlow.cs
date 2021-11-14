using Core.Tasks.Contracts;
using System.Collections.Generic;

namespace Core.Tasks
{
    public class WorkFlow
    {
        private readonly List<ITaskHandler> _tasks = new List<ITaskHandler>();

        public void Append(ITaskHandler task)
        {
            _tasks.Add(task);
        }

        public void Run()
        {
            foreach (var task in _tasks)
            {
                task.Execute();
            }
        }
    }
}

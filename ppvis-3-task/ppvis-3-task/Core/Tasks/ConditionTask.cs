using Core.Tasks.Contracts;
using System;

namespace Core.Tasks
{
    public class ConditionTask : ITaskHandler
    {
        private readonly ITaskHandler _task;
        private readonly Func<bool> _condition;

        public ConditionTask(ITaskHandler task, Func<bool> condition)
        {
            _task = task;
            _condition = condition;
        }

        public void Execute()
        {
            if (_condition.Invoke())
            {
                _task.Execute();
            }
        }
    }
}

using Core.Tasks.Contracts;
using System;

namespace Core.Tasks
{
    public class ApplicationQuitTask : ITaskHandler
    {
        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}

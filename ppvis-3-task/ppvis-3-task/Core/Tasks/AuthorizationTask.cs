using Core.Authorization.Contracts;
using Core.Tasks.Contracts;

namespace Core.Tasks
{
    public class AuthorizationTask : ITaskHandler
    {
        private readonly IAuthorizationService _service;

        public AuthorizationTask(IAuthorizationService service)
        {
            _service = service;
        }

        public void Execute()
        {
            _service.Authorize();
        }
    }
}

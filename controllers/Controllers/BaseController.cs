using Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IServiceManager _serviceManager;

        public BaseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    }
}

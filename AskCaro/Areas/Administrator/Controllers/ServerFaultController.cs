using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AskCaro.Areas.Administrator.Controllers
{
    [Area("Administrator")]

    public class ServerFaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tag()
        {
            return View();
        }

        public IActionResult Statistics()
        {
            return View();
        }


        public IActionResult Questions()
        {
            return View();
        }

        public IActionResult Tags()
        {
            return View();
        }

        public IActionResult AddTag()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Scheduler()
        {
            return View();
        }
    }
}
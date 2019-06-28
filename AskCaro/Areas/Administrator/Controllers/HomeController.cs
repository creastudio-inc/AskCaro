using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AskCaro.Areas.Administrator.Controllers
{
    [Area("Administrator")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
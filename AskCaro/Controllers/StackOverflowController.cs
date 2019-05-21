using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AskCaro.Controllers
{
    public class StackOverflowController : Controller
    {
        public IActionResult Index()
        {
            AskCaro_stackoverflow.Main main = new AskCaro_stackoverflow.Main();
           // main.getlastcount();
            return View();
        }
    }
}
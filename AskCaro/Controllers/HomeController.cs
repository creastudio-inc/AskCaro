using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskCaro.Models;
using AskCaro.MachineLearning.DataStructures;

namespace AskCaro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

           return View();
        }
        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            ChatIssue issue = new ChatIssue() { Question = Question};
            var test = AskCaro.MachineLearning.Program.BuildModel(AskCaro.MachineLearning.Program.ModelPath, issue);
            return Json(new { Answer = test.Answer, More = test.More });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

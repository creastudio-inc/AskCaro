using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskCaro.Models;
using AskCaro.MachineLearning.DataStructures;
using static AskCaro.MachineLearning.Program;

namespace AskCaro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            AskCaro.MachineLearning.Program.train();
            return View();
        }
        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            QuestionsIssue issue = new QuestionsIssue() { ShortDescription = Question,LongDescription =Question};
            var test = AskCaro.MachineLearning.Program.BuildModel(AskCaro.MachineLearning.Program.ModelPath, issue);
            return Json(new { Answer = test.answer });
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

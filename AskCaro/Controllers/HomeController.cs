using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskCaro.Models;
using AskCaro.MachineLearning.DataStructures;
using static AskCaro.MachineLearning.Program;
using Lucene.Net.Analysis.Standard;
using System.Text;
using Lucene.Net.Analysis;
using System.IO;

namespace AskCaro.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            double d = AskCaro_QuestionnaireAspirateur.AnalyzerText.CompareStrings("Q: How can I get the client's IP address in ASP.NET MVC?", "How to get client IP address in MVC 4 controller?");

            return View();
        }


        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            string shorttag = AskCaro_QuestionnaireAspirateur.AnalyzerText.GetTag(Question);
            QuestionsIssue issue = new QuestionsIssue() {  LongDescription = shorttag };
            var test = AskCaro.MachineLearning.Program.BuildModel(@"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\MLModels/GitHubLabelerModel.zip", issue);
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

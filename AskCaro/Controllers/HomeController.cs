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
using Microsoft.AspNetCore.Hosting;

namespace AskCaro.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        { 
            return View();
        }


        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            string shorttag = AskCaro_QuestionnaireAspirateur.AnalyzerText.GetTag(Question);
            QuestionsIssue issue = new QuestionsIssue() {   Question = shorttag };
            var test = AskCaro.MachineLearning.Program.BuildModel(AskCaro.MachineLearning.Program.ModelPath, issue);
            return Json(new { Answer = test.Answer });
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

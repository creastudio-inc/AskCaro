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
            return View();
        }


        public abstract class AnalyzerView
        {
            public abstract string Name { get; }

            public virtual string GetView(TokenStream tokenStream, out int numberOfTokens)
            {
                StringBuilder sb = new StringBuilder();

                Token token = tokenStream.Next();

                numberOfTokens = 0;

                while (token != null)
                {
                    numberOfTokens++;
                    sb.Append(GetTokenView(token));
                    token = tokenStream.Next();
                }

                return sb.ToString();
            }

            protected abstract string GetTokenView(Token token);
        }

        public class TermAnalyzerView : AnalyzerView
        {
            public override string Name
            {
                get { return "Terms"; }
            }

            protected override string GetTokenView(Token token)
            {
                return token.TermText() + " ";
            }
        }

        public string GetTag(string text)
        {
            StandardAnalyzer analyzer = new StandardAnalyzer();

            int termCounter = 0;

            StringBuilder sb = new StringBuilder();

            AnalyzerView view = new TermAnalyzerView();

            StringReader stringReader = new StringReader(text);

            TokenStream tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

            var Text = view.GetView(tokenStream, out termCounter).Trim();
            return Text;
        }

        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            string shorttag = GetTag(Question);
            QuestionsIssue issue = new QuestionsIssue() {  LongDescription = shorttag ,Title= shorttag };
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

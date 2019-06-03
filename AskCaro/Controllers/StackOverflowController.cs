using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AskCaro.Data;
using AskCaro.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskCaro.Controllers
{

    public class Questions {
        public string LongDescription { get;set;}
        public string answer { get;set;}
    }
    public class StackOverflowController : Controller
    {

        public ApplicationDbContext _dbContext;

        public StackOverflowController(ApplicationDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IActionResult Index()
        {
            AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur main = new AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur();
            string countt = main.GetLastPageNumbers();
            int count = int.Parse(countt);
            int simillar = 0;
            for (int i = 0; i < count; i++)
            {
                var item = main.Start(i);
                foreach (var x in item)
                {
                    List<QuestionModel> questionModels = new List<QuestionModel>();

                    QuestionModel questionModel = new QuestionModel();
                    questionModel.Title = x.title;
                    questionModel.Similar = simillar;
                    questionModel.LinkHref = x.hreflink;
                    questionModel.TextDescription = main.GetDescrip(x.hreflink);
                    questionModel.HtmlDescription = main.GetDescripHtml(x.hreflink);
                    questionModel.Answers = new List<AnswerModel>();
                    foreach (var answers in main.GetBestanswers(x.hreflink))
                    {
                        AnswerModel AnswerModel = new AnswerModel();
                        AnswerModel.Htmldescription = answers.Htmldescription;
                        AnswerModel.Textdescription = answers.Textdescription;
                        AnswerModel.voteCount = answers.voteCount;
                        questionModel.Answers.Add(AnswerModel);
                    }
                    var maxZ = questionModel.Answers.Max(obj => obj.voteCount);

                    questionModel.HtmlAnswers = questionModel.Answers.Where(obj => obj.voteCount == maxZ).FirstOrDefault().Htmldescription;

                    // 
                    questionModels.Add(questionModel);
                    AskCaro_QuestionnaireAspirateur.GoogleAspirateur googleAspirateur = new AskCaro_QuestionnaireAspirateur.GoogleAspirateur();
                   var listgoogle=  googleAspirateur.Start(x.title,x.hreflink, "stackoverflow.com");
                    foreach(var google in listgoogle)
                    {
                        QuestionModel questionModelSimilar = new QuestionModel();
                        questionModelSimilar.Title = google.Title;
                        questionModelSimilar.Similar = simillar;
                        questionModelSimilar.LinkHref = google.hreflink;
                        questionModelSimilar.TextDescription = main.GetDescrip(google.hreflink);
                        questionModelSimilar.HtmlDescription = main.GetDescrip(google.hreflink);
                        questionModelSimilar.HtmlAnswers = questionModel.HtmlAnswers;
                        questionModels.Add(questionModelSimilar);
                    }
                    simillar++;
                    _dbContext.Questions.AddRange(questionModels);
                    var result = _dbContext.SaveChanges();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            return View();
        }



        public IActionResult test()
        {
            var Questionslist = _dbContext.Questions;
            var max = Questionslist.Max(x => x.Similar);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Similar");
            dataTable.Columns.Add("Question");
            dataTable.Columns.Add("Answer");
            for (int i = 0; i < max; i++)
            {
                var lists = Questionslist.Where(x => x.Similar == i);
                foreach (var item in lists)
                {
                    if (!String.IsNullOrEmpty(item.TextDescription))
                    {
                        DataRow row = dataTable.NewRow();
                        var result = AskCaro_QuestionnaireAspirateur.AnalyzerText.GetTag(item.TextDescription);
                        row["Question"] = result;
                        row["Similar"] = item.Similar;
                        row["Answer"] = "<div>"+item.HtmlAnswers.Replace("\n", string.Empty).Replace("\r", string.Empty) + "</div>";
                        dataTable.Rows.Add(row);
                    }
                }
            }
            
            CreateCSV(dataTable, @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\Data\Questiontrain.csv", "\t");
            return View();
        }

        //public IActionResult test2()
        //{
        //    var Questionslist = _dbContext.Questions.Include(c => c.Tags).Include(c => c.Answers);
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("LongDescription");
        //    dataTable.Columns.Add("answer");
        //    foreach (var item in Questionslist)
        //    {
        //        if (item.Answers.Count > 0)
        //        {
        //            DataRow row = dataTable.NewRow();
        //            var result = AskCaro_QuestionnaireAspirateur.AnalyzerText.GetTag(item.TextDescription);
        //            row["LongDescription"] = result;
        //            row["answer"] = item.Answers.Last().Description.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(",", string.Empty).Replace("    ", string.Empty);
        //            dataTable.Rows.Add(row);
        //        }
        //    }
        //    CreateCSV(dataTable, @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\Data\peopletest.csv");
        //    return View();
        //}


        public IActionResult train()
        {
            AskCaro.MachineLearning.Program.train();
            return View();
        }
        public void CreateCSV(DataTable dataTable, string filePath, string delimiter = ",")
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                throw new DirectoryNotFoundException($"Destination folder not found: {filePath}");

            var columns = dataTable.Columns.Cast<DataColumn>().ToArray();

            var lines = (new[] { string.Join(delimiter, columns.Select(c => c.ColumnName)) })
              .Union(dataTable.Rows.Cast<DataRow>().Select(row => string.Join(delimiter, columns.Select(c => row[c]))));
          System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}
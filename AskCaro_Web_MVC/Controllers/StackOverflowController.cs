using AskCaro_Web_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskCaro_Web_MVC.Controllers
{
  
    public class StackOverflowController : Controller
    {

        public ApplicationDbContext _dbContext = new ApplicationDbContext();

        public JsonResult index()
        {
            return Json(new { Question = _dbContext.Questions.Count(), Answer = _dbContext.Answers.Count() } , JsonRequestBehavior.AllowGet);
        }


        public ActionResult run()
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
                    var html = AskCaro_Web_MVC.Tools.WordsAnalyzer.RemoveTag(questionModel.HtmlDescription, "<code", "</code>");
                    html = AskCaro_Web_MVC.Tools.WordsAnalyzer.RemoveTag(html, "(", ")");
                    string InnerText = main.GetInnerText(html).Replace("\r", "").Replace("\n","");
                    questionModel.Tag = AskCaro_Web_MVC.Tools.WordsAnalyzer.StringWords2Remove(x.title).Replace("-", " ") + " "+ AskCaro_Web_MVC.Tools.WordsAnalyzer.StringWords2Remove(InnerText);
                    questionModel.Answers = new List<AnswerModel>();
                    foreach (var answers in main.GetBestanswers(x.hreflink))
                    {
                        AnswerModel AnswerModel = new AnswerModel();
                        AnswerModel.Htmldescription = answers.Htmldescription;
                        AnswerModel.Textdescription = answers.Textdescription;
                        AnswerModel.voteCount = answers.voteCount;
                        questionModel.Answers.Add(AnswerModel);
                    }
                    int maxZ = 0;
                    if (questionModel.Answers.Count > 0)
                    {


                        maxZ = questionModel.Answers.Max(obj => obj.voteCount);

                        questionModel.HtmlAnswers = questionModel.Answers.Where(obj => obj.voteCount == maxZ).FirstOrDefault().Htmldescription;
                    }

                     _dbContext.Questions.Add(questionModel);
                    var result = _dbContext.SaveChanges();
                    AskCaro_QuestionnaireAspirateur.GoogleAspirateur googleAspirateur = new AskCaro_QuestionnaireAspirateur.GoogleAspirateur();
                    var listgoogle = googleAspirateur.Start(x.title, x.hreflink, "stackoverflow.com");
                    foreach (var google in listgoogle)
                    {
                        if (google.Title != questionModel.Title)
                        {
                            QuestionModel questionModelSimilar = new QuestionModel();
                            questionModelSimilar.Title = google.Title;
                            questionModelSimilar.Similar = simillar;
                            questionModelSimilar.LinkHref = google.hreflink;
                            questionModelSimilar.TextDescription = main.GetDescrip(google.hreflink);
                            questionModelSimilar.HtmlDescription = main.GetDescrip(google.hreflink);
                            var htmlgoogle = AskCaro_Web_MVC.Tools.WordsAnalyzer.RemoveTag(questionModelSimilar.HtmlDescription, "<code", "</code>");
                            htmlgoogle = AskCaro_Web_MVC.Tools.WordsAnalyzer.RemoveTag(htmlgoogle, "(", ")");
                            string InnerTextgoogle = main.GetInnerText(htmlgoogle).Replace("\r", "").Replace("\n", "");
                            questionModel.Tag = AskCaro_Web_MVC.Tools.WordsAnalyzer.StringWords2Remove(google.Title).Replace("-", " ") + " " + AskCaro_Web_MVC.Tools.WordsAnalyzer.StringWords2Remove(InnerTextgoogle);
                            questionModelSimilar.HtmlAnswers = questionModel.HtmlAnswers;
                            questionModels.Add(questionModelSimilar);
                        }
                       
                    }
                    simillar++;
                    if (simillar % 40 == 0)
                    {
                        System.Threading.Thread.Sleep(10000);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            return View();
        }



        public ActionResult test(int take, int skip)
        {
            var Questionslist = _dbContext.Conversations.Take(take).Skip(skip);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Question");
            dataTable.Columns.Add("Answer");
            foreach (var item in Questionslist)
            {

                DataRow row = dataTable.NewRow();
                row["Question"] = item.Question;
                row["Answer"] = "<div>" + item.HtmlAnswers.Replace("\n", string.Empty).Replace("\r", string.Empty) + "</div>";
                dataTable.Rows.Add(row);

            }


            CreateCSV(dataTable, @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\Data\Questiontrain.csv", "\t");
            return View();
        }


        public ActionResult test2()
        {

            CategoriesModel categoriesModel = new CategoriesModel();
            categoriesModel.Name = "trivia";
            categoriesModel.CreaDate = DateTime.Now;
            List<ConversationsModel> conversationsModels = new List<ConversationsModel>();
            var lines = System.IO.File.ReadLines(@"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\Data\trivia.yml").ToList();
            for (int i = 0; i < lines.Count(); i = i + 2)
            {
                int j = i;
                if (j < lines.Count() - 2)
                {


                    ConversationsModel conversationsModel = new ConversationsModel();
                    conversationsModel.Question = lines[j].Replace("-", "");
                    conversationsModel.HtmlAnswers = lines[++j].Replace("  -", "");
                    conversationsModel.Categories = categoriesModel;
                    conversationsModel.CreaDate = DateTime.Now;
                    conversationsModels.Add(conversationsModel);
                }
            }


            _dbContext.Conversations.AddRange(conversationsModels);
            var resusslt = _dbContext.SaveChanges();
            return View();
        }


        public ActionResult test3()
        {
            CategoriesModel categoriesModel = new CategoriesModel();
            categoriesModel.Name = "asp.net-mvc-4";
            categoriesModel.CreaDate = DateTime.Now;
            List<ConversationsModel> conversationsModels = new List<ConversationsModel>();

            var Questionslist = _dbContext.Questions;
            var max = Questionslist.Max(x => x.Similar);
            for (int i = 0; i < max; i++)
            {
                var lists = Questionslist.Where(x => x.Similar == i);
                foreach (var item in lists)
                {
                    if (!String.IsNullOrEmpty(item.TextDescription) && !String.IsNullOrEmpty(item.HtmlAnswers))
                    {
                        ConversationsModel conversationsModel = new ConversationsModel();
                        conversationsModel.Question = AskCaro_QuestionnaireAspirateur.AnalyzerText.GetTag(item.TextDescription);
                        conversationsModel.HtmlAnswers = "<div>" + item.HtmlAnswers.Replace("\n", string.Empty).Replace("\r", string.Empty) + "</div>";
                        conversationsModel.Categories = categoriesModel;
                        conversationsModel.CreaDate = DateTime.Now;
                        conversationsModels.Add(conversationsModel);
                    }
                }
            }

            _dbContext.Conversations.AddRange(conversationsModels);
            var resusslt = _dbContext.SaveChanges();
            return View();
        }


        public ActionResult train()
        {
            //AskCaro.MachineLearning.Program.train();
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
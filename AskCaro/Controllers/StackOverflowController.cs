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
        public string Title {get;set;}
        public string ShortDescription { get;set;}
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
            AskCaro_stackoverflow.Main main = new AskCaro_stackoverflow.Main();
            string countt = main.GetLastPageNumbers();
            int count = int.Parse(countt);
            for (int i = 0; i < count; i++)
            {
                var item = main.Start(i);
                foreach (var x in item)
                {
                    QuestionModel questionModel = new QuestionModel();
                    questionModel.Title = x.title;
                    questionModel.ShortDescription = x.shortdescript;
                    questionModel.LinkHref = x.hreflink;
                    questionModel.Tags = new List<TagModel>();
                    foreach (var tag in x.tags)
                    {
                        TagModel tagModel = new TagModel();
                        tagModel.Title = tag;
                        questionModel.Tags.Add(tagModel);
                    }
                    questionModel.LongDescription = main.GetDescrip(x.hreflink);

                    questionModel.Answers = new List<AnswerModel>();

                    foreach (var answers in main.Getanswers(x.hreflink))
                    {
                        AnswerModel AnswerModel = new AnswerModel();
                        AnswerModel.Description = answers;
                        questionModel.Answers.Add(AnswerModel);
                    }
                    _dbContext.Questions.Add(questionModel);
                    var result = _dbContext.SaveChanges();
                    System.Threading.Thread.Sleep(10000);

                }
            }

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
                return  token.TermText() +" ";
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

        public IActionResult test()
        {
            var Questionslist = _dbContext.Questions.Include(c => c.Tags).Include(c=>c.Answers);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Title");
            dataTable.Columns.Add("LongDescription");
            dataTable.Columns.Add("answer");
            foreach (var item in Questionslist)
            {
                if (item.Answers.Count > 0)
                {
                    DataRow row = dataTable.NewRow();
                    var result = GetTag(item.Title);
                    row["Title"] = result;             
                    result = GetTag(item.LongDescription.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(",", string.Empty).Replace("    ", string.Empty));
                    row["LongDescription"] = result;
                 row["answer"] = item.Answers.FirstOrDefault().Description.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(",", string.Empty).Replace("    ", string.Empty);
                    dataTable.Rows.Add(row);
                }
              
            }
            CreateCSV(dataTable, @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\Data\people.csv");
            return View();
        }


        public IActionResult test2()
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
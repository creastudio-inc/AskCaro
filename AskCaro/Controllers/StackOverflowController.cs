using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AskCaro.Data;
using AskCaro.Models;
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


        public IActionResult test()
        {
            var Questionslist = _dbContext.Questions.Include(c => c.Tags).Include(c=>c.Answers);
            var Questions = new List<Questions>();
            foreach (var item in Questionslist)
            {
                Questions question = new Questions();
                question.Title = item.Title;
                question.ShortDescription = item.ShortDescription;
                question.LongDescription = item.LongDescription;
                foreach(var answer in item.Answers)
                {
                    question.answer = answer.Description;
                }
                Questions.Add(question);
            }
            WriteCSV(Questions, @"C:\Users\ahmed\source\repos\AskCaro\AskCaro\MachineLearning\Data\people.csv");
            return View();
        }


        public void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskCaro.Data;
using AskCaro.Models;
using Microsoft.AspNetCore.Mvc;

namespace AskCaro.Controllers
{
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
            for(int i = 0; i < count; i++)
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
    }
}
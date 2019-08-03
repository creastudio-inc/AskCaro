using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AskCaro.Data;
using AskCaro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskCaro.Areas.Administrator.Controllers
{
    [Area("Administrator")]

    public class StackOverflowController : Controller
    {

        public ApplicationDbContext _dbContext;

        public StackOverflowController(ApplicationDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            return View();
        }
    

        public IActionResult Questions()
        {
            var Questions = _dbContext.Questions.Where(x => x.SiteClone == "StackOverflow").ToList();
            return View(Questions);
        }

        public IActionResult Tags()
        {
            var Questions = _dbContext.Tags.Where(x => x.SiteClone == "StackOverflow").ToList();
            return View(Questions);
        }

        public IActionResult AddTag()
        {
            return View(new AskCaro.Models.TagModel());
        }

        public IActionResult DetailTag(Guid? id)
        {
            var tag = _dbContext.Tags.Include(x=>x.TagQuestionModels).ThenInclude(x=>x.QuestionModel).Where(x => x.TagId == id.Value).FirstOrDefault();
            var thread = Program.threads.FirstOrDefault(x => x.Name == tag.Name);
            if (thread!=null && thread.IsAlive)
            {
                ViewBag.IsAlive = true;
            }
            else
            {
                ViewBag.IsAlive = false;

            }
            return View(tag);
        }
        [HttpPost]
        public IActionResult AddTag(AskCaro.Models.TagModel tagModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tagModel);

            }
            tagModel.SiteClone = "StackOverflow";
            tagModel.CreaDate = DateTime.Now;
          _dbContext.Tags.Add(tagModel);
            var re = _dbContext.SaveChanges();
            return RedirectToAction("Tags");
        }


        
        [HttpPost]
        public IActionResult StardThread(Guid TagId)
        {
             Thread thread = new Thread(() => download(TagId));
            thread.IsBackground = true;
            thread.Start();
            Program.threads.Add(thread);
            return View();
        }   
        [HttpPost]
        public IActionResult StopThread(Guid TagId)
        {
            var tag = _dbContext.Tags.Where(x => x.TagId == TagId).FirstOrDefault();
            var thread = Program.threads.FirstOrDefault(x => x.Name == tag.Name);
            thread.Abort();
            Program.threads.Remove(thread);
            return View();
        }

        private void download(Guid TagId)
        {
            var tag = _dbContext.Tags.Where(x => x.TagId == TagId).FirstOrDefault();

            AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur main = new AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur();
              int simillar = 0;
            for (int i = tag.PaginationActuel; i < tag.PaginationFound; i++)
            {
                var item = main.Start(i,tag.Name);
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
                    int maxZ = 0;
                    if (questionModel.Answers.Count > 0)
                    {


                        maxZ = questionModel.Answers.Max(obj => obj.voteCount);

                        questionModel.HtmlAnswers = questionModel.Answers.Where(obj => obj.voteCount == maxZ).FirstOrDefault().Htmldescription;
                    }

                    questionModels.Add(questionModel);
                    AskCaro_QuestionnaireAspirateur.GoogleAspirateur googleAspirateur = new AskCaro_QuestionnaireAspirateur.GoogleAspirateur();
                    var listgoogle = googleAspirateur.Start(x.title, x.hreflink, "stackoverflow.com");
                    foreach (var google in listgoogle)
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
                    if (simillar % 40 == 0)
                    {
                        System.Threading.Thread.Sleep(10000);
                    }
                    _dbContext.Questions.AddRange(questionModels);
                     tag.PaginationActuel = i;
                    _dbContext.Tags.Update(tag);
                    var result = _dbContext.SaveChanges();
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
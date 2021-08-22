using AskCaro_Web_MVC.Models;
using AskCaro_Web_MVC.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskCaro_Web_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WhatIs()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Process()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult AboutCaro()
        {
            return View();
        }
        public ActionResult Picture()
        {
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult SearchAvance()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur main = new AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur();

            var target = Question;
            var text = AskCaro_Web_MVC.Tools.WordsAnalyzer.StringWords2Remove(target);
            var targetHS = text.Split(' ').ToHashSet();

            var closeNeighbors = from h in _dbContext.Questions.AsEnumerable() // bring into memory
                                                                               // query continued below as linq-to-objects

                                 let score = (0.15 * LevenshteinDistance.Calculate(text, h.Tag) + 0.35 * LevenshteinDistance.Calculate(text.ToLower(), h.Tag.ToLower())) / Math.Max(text.Length, h.Tag.Length)
                                 //let lD = Tools.Class1.LevenshteinDistance(target, h.Tag)
                                 //let length = Math.Max(h.Tag.Length, target.Length)
                                 //let score = 1.0 - (double)lD / length 
                                 where score > 0.25
                                 select new { h, score };
            var listttt = closeNeighbors.OrderByDescending(x => x.score).First();
            return Json(new { Answer = listttt.h.HtmlAnswers });
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Index22()
        {
            AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur main = new AskCaro_QuestionnaireAspirateur.StackOverflowAspirateur();

            var target = "Is there a best practice and recommended alternative to Session variables in MVC";
            var text = AskCaro_Web_MVC.Tools.WordsAnalyzer.StringWords2Remove(target);
            var targetHS = text.Split(' ').ToHashSet();
  
            var closeNeighbors = from h in _dbContext.Questions.AsEnumerable() // bring into memory
                                                                               // query continued below as linq-to-objects
                                                           
                                 let score = (0.15 * LevenshteinDistance.Calculate(text, h.Tag) +    0.35 * LevenshteinDistance.Calculate(text.ToLower(), h.Tag.ToLower())) / Math.Max(text.Length, h.Tag.Length)
                                 //let lD = Tools.Class1.LevenshteinDistance(target, h.Tag)
                                 //let length = Math.Max(h.Tag.Length, target.Length)
                                 //let score = 1.0 - (double)lD / length 
                                 where score>0.25                  
                                 select new { h , score } ;
            var listttt = closeNeighbors.OrderByDescending(x=>x.score).Take(10).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
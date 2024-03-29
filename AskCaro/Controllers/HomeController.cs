﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskCaro.Models; 

namespace AskCaro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           // AskCaro.MachineLearning.Program.train();
            return View();
        }
        public IActionResult WhatIs()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Process()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult AboutCaro()
        {
            return View();
        }
        public IActionResult Picture()
        {
            return View();
        }
        public IActionResult Chat()
        {
            return View();
        }
        public IActionResult SearchAvance()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ReponseCaro(String Question)
        {
            //QuestionsIssue issue = new QuestionsIssue() { Question = Question};
            //var test = AskCaro.MachineLearning.Program.BuildModel(AskCaro.MachineLearning.Program.ModelPath, issue);
            return Json(new { Answer = Question });
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

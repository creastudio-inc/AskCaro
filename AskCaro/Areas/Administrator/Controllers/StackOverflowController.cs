using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskCaro.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Scheduler()
        {
            return View();
        }
    }
}
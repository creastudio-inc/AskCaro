using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AskCaro_stackoverflow
{
    public static class Property
    {
        public static List<string> AllLinksPages = new List<string>();
        public static String RacineURL = "http://store.root.sa/saveroom/dev/";
        public static String RacinePathViews = String.Empty;
        public static String RacinePathControllers = String.Empty;
        public static String RacinePathContent = String.Empty;
        public static Boolean IsFinish = false;
    }
    public class stackoverflow
    {
        public string hreflink {get;set;}
        public string shortdescript {get;set;}
        public List<string> tags { get; set; }
    }
    public class Main
    {
        public void Start()
        {
            var doc = new HtmlWeb().Load("https://stackoverflow.com/questions/tagged/asp.net-core?sort=votes&page=1&pagesize=15");
            List<stackoverflow> stackoverflow = new List<stackoverflow>();
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='summary']"))
            {
                stackoverflow stflow = new stackoverflow();
                foreach (var node in link.ChildNodes)
                {                  
                    if (node.Name == "h3")
                    {
                        var a_href = node.ChildNodes[0];
                        foreach (var att in a_href.Attributes)
                        {
                            if (att.Name == "href")
                            {
                                stflow.hreflink = "https://stackoverflow.com" + att.Value;
                            }
                        }
                    }
                    if (node.Name == "div")
                    {
                        foreach (var att in node.Attributes)
                        {
                            if (att.Name == "class" && att.Value== "excerpt")
                            {
                                stflow.shortdescript = node.InnerHtml;
                            }

                            if ( att.Value.Contains("tags"))
                            {
                                stflow.tags = GetTag(node.ChildNodes);
                            }
                        }
                    }
                }
                stackoverflow.Add(stflow);
            }
            Property.IsFinish = true;
        }

        public string GetLastPageNumbers()
        {
            var doc = new HtmlWeb().Load("https://stackoverflow.com/questions/tagged/asp.net-core?sort=votes&page=1&pagesize=15");
            var allhref = doc.DocumentNode.SelectNodes("//div[@class='pager fl']//a");
            return allhref[allhref.Count - 1].InnerText;
        }

        private List<string> GetTag(HtmlNodeCollection childNodes)
        {
            List<string> tag = new List<string>();
            foreach (var node in childNodes)
            {
                if (node.Name == "a")
                {
                    tag.Add(node.InnerText);
                }
                }
            return tag;
        }

      
    }
}
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AskCaro_stackoverflow
{
    public static class Property
    {
 
    }
    public class stackoverflow
    {
        public string title {get;set;}
        public string hreflink {get;set;}
        public string shortdescript {get;set;}
        public List<string> tags { get; set; }
    }
    public class Main
    {
        public List<stackoverflow> Start(int i)
        {
            var doc = new HtmlWeb().Load("https://stackoverflow.com/questions/tagged/asp.net-mvc-4?sort=votes&page=" + i+"&pagesize=15");
            List<stackoverflow> stackoverflow = new List<stackoverflow>();
            var summary = doc.DocumentNode.SelectNodes("//div[@class='summary']");
            if (summary != null)
            {

            
            foreach (HtmlNode link in summary)
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
                        stflow.title = node.InnerText;
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
            }
            return stackoverflow;
        }

        public string GetLastPageNumbers()
        {
            try
            {
                var doc = new HtmlWeb().Load("https://stackoverflow.com/questions/tagged/asp.net-core?sort=votes");
                var allhref = doc.DocumentNode.SelectNodes("//div[@class='pager fl']//a");
                    return allhref[allhref.Count - 2].InnerText;
            }
            catch(Exception ex) {
                return "0";
            }
        
              
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


        public string GetDescrip(String link)
        {
            var doc = new HtmlWeb().Load(link);
            var question = doc.DocumentNode.SelectNodes("//div[@class='postcell post-layout--right']//div[@class='post-text']");
            if (question!=null && question.Count > 0)
                return question[0].InnerText;
            else
            {
                return " ";
            }
 
         }
        public List<string> Getanswers(String link)
        {
            List<string> ans = new List<string>();
            var doc = new HtmlWeb().Load(link);
            var answers = doc.DocumentNode.SelectNodes("//div[@id='answers']//div[@class='answer']//div[@class='post-layout']//div[@class='answercell post-layout--right']//div[@class='post-text']");
            if (answers != null)
            {
                foreach (HtmlNode answer in answers)
                {
                    ans.Add(answer.InnerText);
                }
            }
            
            return ans;
         }

    }
}
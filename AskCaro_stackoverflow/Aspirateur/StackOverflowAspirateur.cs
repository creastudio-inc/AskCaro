using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AskCaro_QuestionnaireAspirateur
{
    public class StackOverflowAspirateur
    {
        public List<stackoverflowModel> Start(int i,string tagname)
        {
            var url = String.Format(URLTags.StackOverflow, tagname, "15",i);
            var doc = new HtmlWeb().Load(url);
            List<stackoverflowModel> stackoverflow = new List<stackoverflowModel>();
            var summary = doc.DocumentNode.SelectNodes("//div[@class='summary']");
            if (summary != null)
            {        
            foreach (HtmlNode link in summary)
            {
                    stackoverflowModel stflow = new stackoverflowModel();
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
        public string GetDescripHtml(String link)
        {
            var doc = new HtmlWeb().Load(link);
            var question = doc.DocumentNode.SelectNodes("//div[@class='postcell post-layout--right']//div[@class='post-text']");
            if (question != null && question.Count > 0)
                return question[0].InnerHtml;
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

        public List<stackoverfloAnswerswModel> GetBestanswers(String link)
        {
            List<stackoverfloAnswerswModel> ans = new List<stackoverfloAnswerswModel>();
            var doc = new HtmlWeb().Load(link);
            var answers = doc.DocumentNode.SelectNodes("//div[@id='answers']//div[contains(@class,'answer')]//div[@class='post-layout']");
            if (answers != null)
            {
                foreach (HtmlNode answer in answers)
                {
                    stackoverfloAnswerswModel stackoverfloAnswerswModel = new stackoverfloAnswerswModel();
                    foreach( var child in answer.ChildNodes)
                    {
                        if (child.Name == "div")
                        {
                            foreach(var att in child.Attributes)
                            {
                                if(att.Name=="class" && att.Value== "answercell post-layout--right")
                                {
                                    foreach(var childd in child.ChildNodes)
                                    {
                                        if (childd.Attributes.Count > 1)
                                        {
                                            stackoverfloAnswerswModel.Htmldescription = childd.InnerHtml;
                                            stackoverfloAnswerswModel.Textdescription = childd.InnerText.Replace("\r\n", "");
                                        }
                                    }
                                 
                                }

                                if (att.Name=="class" && att.Value== "votecell post-layout--left")
                                {
                                    stackoverfloAnswerswModel.voteCount = Int32.Parse(child.InnerText.Replace("\r\n","").Replace(" ", "").Replace("+", ""));
                                }
                            }
                        }
                    }
                    if(stackoverfloAnswerswModel.Textdescription!=null)
                        ans.Add(stackoverfloAnswerswModel);


                }
            }
                return ans;
        }

    }
}
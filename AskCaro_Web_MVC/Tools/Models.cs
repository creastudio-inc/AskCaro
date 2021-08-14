using System;
using System.Collections.Generic;
using System.Text;

namespace AskCaro_QuestionnaireAspirateur
{
    public static class URLSearch
    {
        public static string CodeProject = "https://www.codeproject.com/search.aspx?q={0}&doctypeid=4";
        public static string StackOverflow = "https://stackoverflow.com/search?q={0}";
        public static string StackExchange = "https://softwareengineering.stackexchange.com/search?q={0}";
        public static string ServerFault = "https://serverfault.com/search?q={0}";
        public static string Google = "https://www.google.com/search?q={0}+site:{1}";
    }

    public static class URLTags
    {
        public static string StackOverflow = "https://stackoverflow.com/questions/tagged/{0}?sort=votes&pageSize={1}&page={2}";
        public static string StackExchange = "https://softwareengineering.stackexchange.com/questions/tagged/{0}?sort=votes&pageSize={1}&page={2}";
        public static string ServerFault = "https://serverfault.com/questions/tagged/{0}?sort=votes&pageSize={1}&page={2}";
    }

    // 
    public class stackoverflowModel
    {
        public string title { get; set; }
        public string hreflink { get; set; }
        public string shortdescript { get; set; }
    }
    public class stackoverfloAnswerswModel
    {
        public int voteCount { get; set; }
        public string Htmldescription { get; set; }
        public string Textdescription { get; set; }
    }

    public class GoogleModel
    {
        public string Title { get; set; }
        public string hreflink { get; set; }
    }
}

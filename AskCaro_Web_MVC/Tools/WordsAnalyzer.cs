using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AskCaro_Web_MVC.Tools
{
    public static class WordsAnalyzer
    {
        private static List<string> wordsToRemove = "I A IS ABOUT ABOVE AFTER AGAIN AGAINST ALL JUST TEXT OF AND IT THE DE DA DAS DO DOS AN NAS NO NOS EM E A AS O OS AO AOS P LDA AND".Split(' ').ToList();

        public static string StringWordsRemove(string stringToClean)
        {
            return string.Join(" ", stringToClean.Split(' ').Except(wordsToRemove));
        }
        public static string StringWords2Remove(string stringToClean)
        {
            // Define how to tokenize the input string, i.e. space only or punctuations also
            return string.Join(" ", stringToClean
                .Split(new[] { ' ', ',', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                .Except(wordsToRemove));
        }
        public static string StringWordsRemove(string stringToClean, string wordsToRemove)
        {
            string[] splitWords = wordsToRemove.Split(new Char[] { ' ' });
            string pattern = " (" + string.Join("|", splitWords) + ") ";
            string cleaned = Regex.Replace(stringToClean, pattern, " ");
            return cleaned;
        }

        public static string Letters(this string input)
        {
            return string.Concat(input.Where(x => char.IsLetter(x) && !char.IsSymbol(x) && !char.IsWhiteSpace(x)));
        }



        // na9ra code html amta3 question 
        // ni7 tous affichier amta3 code balise 
        // ni7i les wordtoremove eli lazim yitna7aw
        // na3Mal analyse



        // How to remove the text between the tags in c#

        private static readonly Regex _tags_ = new Regex(@"<[^>]+?>", RegexOptions.Multiline | RegexOptions.Compiled);

        //add characters that are should not be removed to this regex
        private static readonly Regex _notOkCharacter_ = new Regex(@"[^\w;&#@.:/\\?=|%!() -]", RegexOptions.Compiled);

        public static String UnHtml(String html)
        {
            html = HttpUtility.UrlDecode(html);
            html = HttpUtility.HtmlDecode(html);

            html = RemoveTag(html, "<!--", "-->");
            html = RemoveTag(html, "<script", "</script>");
            html = RemoveTag(html, "<style", "</style>");

            //replace matches of these regexes with space
            html = _tags_.Replace(html, " ");
            html = _notOkCharacter_.Replace(html, " ");
            html = SingleSpacedTrim(html);

            return html;
        }

        public static String RemoveTag(String html, String startTag, String endTag)
        {
            Boolean bAgain;
            do
            {
                bAgain = false;
                Int32 startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
                if (startTagPos < 0)
                    continue;
                Int32 endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
                if (endTagPos <= startTagPos)
                    continue;
                html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
                bAgain = true;
            } while (bAgain);
            return html;
        }

        private static String SingleSpacedTrim(String inString)
        {
            StringBuilder sb = new StringBuilder();
            Boolean inBlanks = false;
            foreach (Char c in inString)
            {
                switch (c)
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        if (!inBlanks)
                        {
                            inBlanks = true;
                            sb.Append(' ');
                        }
                        continue;
                    default:
                        inBlanks = false;
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString().Trim();
        }
    }
}
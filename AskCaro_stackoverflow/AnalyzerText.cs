using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AskCaro_QuestionnaireAspirateur
{
    public abstract class AnalyzerView
    {
        public abstract string Name { get; }

        public virtual string GetView(TokenStream tokenStream, out int numberOfTokens)
        {
            StringBuilder sb = new StringBuilder();

            Token token = tokenStream.Next();

            numberOfTokens = 0;

            while (token != null)
            {
                numberOfTokens++;
                sb.Append(GetTokenView(token));
                token = tokenStream.Next();
            }

            return sb.ToString();
        }

        protected abstract string GetTokenView(Token token);
    }

    public class TermAnalyzerView : AnalyzerView
    {
        public override string Name
        {
            get { return "Terms"; }
        }

        protected override string GetTokenView(Token token)
        {
            return token.TermText() + " ";
        }
    }

    public static class AnalyzerText{
        public static string GetTag(string text)
    {
            text = text.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(",", string.Empty).Replace("    ", string.Empty);
        StandardAnalyzer analyzer = new StandardAnalyzer();

        int termCounter = 0;

        StringBuilder sb = new StringBuilder();

        AnalyzerView view = new TermAnalyzerView();

        StringReader stringReader = new StringReader(text);

        TokenStream tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

        var Text = view.GetView(tokenStream, out termCounter).Trim();
        return Text;
    }

   
            public static double CompareStrings(string strA, string strB)
            {
                List<string> setA = new List<string>();
                List<string> setB = new List<string>();

                for (int i = 0; i < strA.Length - 1; ++i)
                    setA.Add(strA.Substring(i, 2));

                for (int i = 0; i < strB.Length - 1; ++i)
                    setB.Add(strB.Substring(i, 2));

                var intersection = setA.Intersect(setB, StringComparer.InvariantCultureIgnoreCase);

                return (2.0 * intersection.Count()) / (setA.Count + setB.Count);
            }
        
    }
}

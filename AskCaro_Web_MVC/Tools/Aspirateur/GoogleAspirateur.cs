using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskCaro_QuestionnaireAspirateur
{
    public class GoogleAspirateur
    {
        public List<GoogleModel> Start(string title,string linksite, string site)
        {
            System.Threading.Thread.Sleep(10000);
            var url = String.Format(URLSearch.Google, title, site);
            var doc = new HtmlWeb().Load(url);
            List<GoogleModel> GoogleModel = new List<GoogleModel>();
            List<string> alllink = new List<string>();
            var summary = doc.DocumentNode.SelectNodes("//html//body//div//a"); 
            if (summary != null)
            {
                foreach (HtmlNode link in summary)
                {
                    List<double> pourcentage = new List<double>();
                    var hreflink = "";
                    foreach ( var href in link.Attributes)
                    {
                        if(href.Name=="href" && href.Value.Contains("/url?q=https://" + site))
                        {
                            hreflink = href.Value.Remove(0, 7).Split(';').Where(x=>x.Contains(site)).FirstOrDefault();
                            if (hreflink.Contains("&amp")) hreflink = hreflink.Remove(hreflink.Length - 4, 4);
                            if (hreflink.Contains("/api.stackexchange.com")) hreflink = hreflink.Remove(hreflink.Length - 22, 22);
                            double hreflinkcount = AnalyzerText.CompareStrings(linksite, hreflink);
                            if (hreflinkcount < 0.8)
                            {
                                alllink.Add(hreflink);
                                double d = AnalyzerText.CompareStrings(title, link.InnerText);
                                pourcentage.Add(d);
                                foreach (var ChildNode in link.ChildNodes)
                                {
                                if (ChildNode.Name == "div")
                                {
                                    d = AnalyzerText.CompareStrings(title, ChildNode.InnerText);
                                    pourcentage.Add(d);
                                }
                            }
                            }
                        }
                    }
                   if(pourcentage.Count>0&& pourcentage.Max() > 0.2)
                    {
                        GoogleModel.Add(new AskCaro_QuestionnaireAspirateur.GoogleModel() {hreflink = hreflink });
                    }

                }
            }
            return GoogleModel;
        }

    }
}

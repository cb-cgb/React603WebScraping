using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using System.Net.Http;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace React603WebScraping.web.Scraping
{

    
    public static class ScrapingManager

    {
        public static string GetHtml()
        {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("user-agent", "pulling TLS");
            return client.GetStringAsync("http://thelakewoodscoop.com/").Result;
        }

        public static List<TLSResult> GetPosts()
        {
            var result = new List<TLSResult>();
            var html = GetHtml();
            var parser = new HtmlParser();
            IHtmlDocument htmlDocument = parser.ParseDocument(html);
            var divs = htmlDocument.QuerySelectorAll("div.post");
            foreach (var div in divs)
            {
                var tlsItem = ParseDiv(div);
                if (tlsItem != null)
                {
                    result.Add(tlsItem);
                }
            }

            return result;
        }

        private static TLSResult ParseDiv(IElement div)
        {


            var item = new TLSResult();
            var h2 = div.QuerySelector("h2 a");
            if (h2 == null)
            {
                return null;
            }
            item.Title = h2.TextContent;


            //var aTag = h2.QuerySelector("a");
            //if (aTag == null)
            //{
            //    return null;
            //}
            item.Url = h2.Attributes["href"].Value;

            var pTag = div.QuerySelector("p");
            if (pTag == null)
            {
                return null;
            }
            var summary = pTag.TextContent;
            item.Summary = SummaryWithoutReadMore(summary);

            var imgTag = div.QuerySelector("p a img");
            if (imgTag == null)
            {
                return null;
            }
            item.Image = imgTag.Attributes["src"].Value;

            var commentTag = div.QuerySelector(".backtotop");
            if (commentTag == null)
            {
                return null;
            }
            item.CommentCount = commentTag.TextContent;

            return item;
        }

        private static string SummaryWithoutReadMore(string summary)
        {
            var indexofReadMore = summary.IndexOf("Read more");
            if (indexofReadMore == -1)
            {
                return summary;
            }
            return summary.Substring(0, indexofReadMore); 
       
        }
    }
}

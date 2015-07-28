using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;

    class WebCrawler
    {
        private List<string> crawledPages = null;

        private List<string> siteMap = null;

        public List<string> StartCrawl(string uri)
        {
            this.crawledPages = new List<string> { uri };
            this.siteMap = new List<string>();

            var page = new WebPage("http://wiprodigital.com/");

            Console.WriteLine("Starting Crawl with: " + uri);
            this.Crawl(page);

            return crawledPages;
        }

        private void Crawl(WebPage page)
        {
            var uris = page.GetFilteredUris(this.UriFilter);
            var pageList = new List<WebPage>();

            uris.ForEach(
                uri =>
                    {
                        if (crawledPages.Contains(uri))
                        {
                            Console.WriteLine("Already crawled to: " + uri);
                            return;
                        }

                        Console.WriteLine(uri);
                        pageList.Add(new WebPage(uri));
                        
                        this.crawledPages.Add(uri);
                    });

            pageList.ForEach(this.Crawl);
        }

        private bool UriFilter(string uri)
        {
            if (uri.Contains("wiprodigital.com"))
            {
                return true;
            }

            Console.WriteLine("Won't crawl to uri as its out of the domain:" + uri);
            return false;
        }
    }
}

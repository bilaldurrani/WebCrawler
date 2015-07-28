using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler
{
    class WebCrawler
    {
        private List<string> crawledPages;

        public List<string> StartCrawl(string uri)
        {
            this.crawledPages = new List<string> { uri };

            var page = new WebPage("http://wiprodigital.com/");

            Console.WriteLine("Starting Crawl with: " + uri);
            this.Crawl(page);

            return this.crawledPages;
        }

        private void Crawl(WebPage page)
        {
            Console.WriteLine("Crawling to: {0}", page.Address);

            var uris = page.GetFilteredUris(this.UriFilter);
            var pageList = new List<WebPage>();

            uris.ForEach(
                uri =>
                    {
                        // Checking if we have already been to this page to avoid cirular loop.
                        if (crawledPages.Contains(uri))
                        {
                            return;
                        }

                        pageList.Add(new WebPage(uri));
                        this.crawledPages.Add(uri);
                    });

            Parallel.ForEach(pageList, this.Crawl);
        }

        private bool UriFilter(string uri)
        {
            // Simple check for domain. Should use Regex.
            if (uri.Contains("wiprodigital.com"))
            {
                return true;
            }

            //Won't crawl to uri as its out of the domain.
            return false;
        }
    }
}

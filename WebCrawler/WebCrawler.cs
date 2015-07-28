using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawler
{
    class WebCrawler
    {
        private List<string> crawledPages;
        private List<string> externalPages;

        public void StartCrawl(string uri, out List<string> internalPages, out List<string> otherPages)
        {
            this.crawledPages = new List<string> { uri };
            this.externalPages = new List<string>();

            var page = new WebPage("http://wiprodigital.com/");

            Console.WriteLine("Starting Crawl with: " + uri);
            this.Crawl(page);

            internalPages = this.crawledPages;
            otherPages = this.externalPages;
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
            if (!this.externalPages.Contains(uri))
            {
                this.externalPages.Add(uri);
            }

            return false;
        }
    }
}

using System;

namespace WebCrawler
{
    using System.Collections.Generic;

    class Program
    {
        private static void Main()
        {
            List<string> internalPages;
            List<string> externalPages;

            var webCrawler = new WebCrawler();
            webCrawler.StartCrawl(@"http://wiprodigital.com/", out internalPages, out externalPages);

            Console.WriteLine("______________________________");
            Console.WriteLine("SITE MAP - Internal Pages. Count: {0}", internalPages.Count);
            Console.WriteLine("______________________________");
            internalPages.ForEach(Console.WriteLine);
            Console.WriteLine("______________________________");
            Console.WriteLine("SITE MAP - External Pages. Count: {0}", externalPages.Count);
            Console.WriteLine("______________________________");
            externalPages.ForEach(Console.WriteLine);
            Console.WriteLine("______________________________");

            Console.ReadKey();
        }
    }
}

using System;

namespace WebCrawler
{
    class Program
    {
        private static void Main()
        {
            var webCrawler = new WebCrawler();
            var siteMap = webCrawler.StartCrawl(@"http://wiprodigital.com/");

            Console.WriteLine("______________________________");
            Console.WriteLine("SITE MAP");
            Console.WriteLine("______________________________");
            siteMap.ForEach(Console.WriteLine);
            Console.WriteLine("______________________________");

            Console.ReadKey();
        }
    }
}

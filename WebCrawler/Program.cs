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

    class Program
    {
        private static void Main(string[] args)
        {
            var webCrawler = new WebCrawler();
            var siteMap = webCrawler.StartCrawl(@"http://wiprodigital.com/");

            Console.WriteLine(siteMap);
        }
    }
}

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

    using Microsoft.SqlServer.Server;

    class WebPage
    {
        private string address;

        public WebPage(string address)
        {
            this.address = address;
        }

        public string GetBody()
        {
            var request = WebRequest.Create(this.address);

            request.GetRequestStreamAsync();
            request.Method = "GET";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (WebException ex)
            {
                Console.WriteLine("The uri is not reachable: {0}, Status: {1}", this.address, ex.Status);
                return string.Empty;
            }
        }

        public List<string> GetUris()
        {
            var body = this.GetBody();

            var regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
            var matches = regx.Matches(body);

            return (from Match match in matches select match.Value).ToList();
        }

        public List<string> GetFilteredUris(Predicate<string> filter)
        {
            var allUris = this.GetUris();

            return allUris.Where(uri => filter(uri)).ToList();
        }
    }
}

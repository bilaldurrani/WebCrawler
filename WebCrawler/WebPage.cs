using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace WebCrawler
{
    class WebPage
    {
        public string Address { get; set; }
        
        public WebPage(string address)
        {
            this.Address = address;
        }

        private string GetBody()
        {
            var request = WebRequest.Create(this.Address);
            request.GetRequestStreamAsync();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (WebException)
            {
                // Uri might not be reachable if its a resource. Can optimize by not trying to go to resources.
                return string.Empty;
            }
        }

        private List<string> GetUris()
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

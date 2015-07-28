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

    class WebPage
    {
        private Uri address;

        public WebPage(Uri address)
        {
            this.address = address;
        }

        public string GetBody()
        {
            var request = WebRequest.Create(address);

            request.GetRequestStreamAsync();
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public string Get
    }
}

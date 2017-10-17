using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WatsonConversationClient.Helpers
{
    public class NetworkHelper
    {
        public static HttpClient ConstruirHttpClient(bool useProxy, string proxyAddress, string proxyPort, string proxyUser, string proxyPassword)
        {
            if (useProxy)
            {
                string proxyUri =
                   string.Format("{0}:{1}", proxyAddress, proxyPort);

                NetworkCredential proxyCreds = new NetworkCredential(
                    proxyUser,
                    proxyPassword
                );

                WebProxy proxy = new WebProxy(proxyUri, false)
                {
                    UseDefaultCredentials = false,
                    Credentials = proxyCreds,
                };

                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };

                httpClientHandler.Credentials = proxyCreds;

                return new HttpClient(httpClientHandler);
            }
            else
            {
                return new HttpClient();
            }
        }
    }
}

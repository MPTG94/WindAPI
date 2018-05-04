using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace WindAPI.Tests.Handlers
{
    /// <summary>
    /// Class to handle Http Request Creation for tests.
    /// </summary>
    class HttpRequestHandler
    {
        public static HttpRequestMessage GenerateHttpRequestMessage()
        {
            var requestMessage = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            return requestMessage;
        }
    }
}

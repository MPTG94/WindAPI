using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace WindAPI.Classes
{
    /// <summary>
    /// This class inherits the default JsonMediaTypeFormatter and
    /// makes sure the returned response header when making requests is application/json.
    /// It also makes sure the JSON returned when browsing the API with a web browser is pretty printed
    /// </summary>
    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
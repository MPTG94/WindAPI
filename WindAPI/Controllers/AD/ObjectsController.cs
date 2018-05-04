using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WindAPI.Classes.ResultObjects;
using WindAPI.Handlers;

namespace WindAPI.Controllers.AD
{
    [RoutePrefix("api/ad/Objects")]
    public class ObjectsController : ApiController
    {
        // GET: api/ad/Objects
        /// <summary>
        /// A basic GET request to the API to check if the API is alive.
        /// </summary>
        /// <returns>A welcome string.</returns>
        [Route("")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string name = GetType().Name;
            response = Request.CreateResponse(HttpStatusCode.OK, $"Welcome to Wind API! - { name }");

            return response;
        }

        // GET: api/Users/CheckADObject?adObject=[username/groupname/computername]
        /// <summary>
        /// Gets the name of a user, group or computer and returns it's object type in AD.
        /// </summary>
        /// <param name="adObject">The name of the object to check in AD.</param>
        /// <returns>An HttpResponseMessage containing an ADObjectCheckResult object with
        /// the name of the object to check, a
        /// code to determine the result type and a list explaining the type of input object
        /// in AD.</returns>
        [HttpGet]
        [Route("{adObject}")]
        public HttpResponseMessage CheckADObject(string adObject)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            ADObjectCheckResult result = new ADObjectCheckResult();
            result = ActiveDirectory.CheckADObjectType(adObject);

            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}

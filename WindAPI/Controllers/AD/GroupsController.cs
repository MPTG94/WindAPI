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
    /// <summary>
    /// The controller providing API Endpoints to query Active Directory Groups.
    /// </summary>
    [RoutePrefix("api/ad/groups")]
    public class GroupsController : ApiController
    {
        // GET: api/ad/Groups
        // GET: api/ad/Groups/?groups=[groupname]&groups=[groupname]
        /// <summary>
        /// A basic GET request to the API to check if the API is alive.
        /// 
        /// This endpoint also gets a group name or multiple group names
        /// and returns relevant group object info.
        /// </summary>
        /// <param name="groups">A group name or multiple group names
        /// as they are listed in Active Directory.</param>
        /// <returns>A welcome string. or The input groupname(s) and additional relevant AD fields.</returns>
        [Route("")]        
        public HttpResponseMessage Get([FromUri]string[] groups = null)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (groups != null)
            {
                if (groups.Length != 0)
                {
                    // Creating list for results
                    List<GroupInfoResult> resultList = new List<GroupInfoResult>();
                    // Iterating over each group.
                    foreach (string user in groups)
                    {
                        GroupInfoResult result = new GroupInfoResult();
                        result.sAMAccountName = user;
                        result = ActiveDirectory.GetGroupInfoByGroupname(result);
                        resultList.Add(result);
                        response = Request.CreateResponse(HttpStatusCode.OK, resultList);
                    }
                }
                else
                {
                    // No groups entered.
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, $"Welcome to Wind API! - { GetType().Name }");
            }

            return response;
        }

        // GET: api/ad/Groups/[groupname]
        /// <summary>
        /// Gets a group sAMAccountName and returns relevant group object info.
        /// </summary>
        /// <param name="group">A group sAMAccountName in Active Directory.</param>
        /// <returns>The input group sAMAccountName and additional relevant AD fields.</returns>
        [HttpGet]
        [Route("{group}")]
        public HttpResponseMessage Get(string group)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (group != string.Empty)
            {
                GroupInfoResult result = new GroupInfoResult();
                result.sAMAccountName = group;
                result = ActiveDirectory.GetGroupInfoByGroupname(result);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                // No groups entered.
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }
    }
}

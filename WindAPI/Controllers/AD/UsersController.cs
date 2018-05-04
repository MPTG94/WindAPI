using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WindAPI.Classes.RequestObjects;
using WindAPI.Classes.ResultObjects;
using WindAPI.Config;
using WindAPI.Handlers;

namespace WindAPI.Controllers.AD
{
    /// <summary>
    /// The controller providing API Endpoints to query Active Directory Users.
    /// </summary>
    [RoutePrefix("api/ad/users")]
    public class UsersController : ApiController
    {
        // GET: api/ad/Users
        // GET: api/ad/Users/GetUserInfo?users=[username]&users=[username2]&users=[username3]
        /// <summary>
        /// A basic GET request to the API to check if the API is alive.
        /// </summary>
        /// <param name="users">A list of usernames to get info for, supplied as queryparams</param>
        /// <returns>A welcome string.</returns>
        [Route("")]
        public HttpResponseMessage Get([FromUri]string[] users = null)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            if (users != null)
            {
                if (users.Length != 0)
                {
                    // Creating list for results
                    List<UserInfoResult> resultList = new List<UserInfoResult>();
                    // Iterating over each user.
                    foreach (string user in users)
                    {
                        UserInfoResult result = new UserInfoResult();
                        result.sAMAccountName = user;
                        result = ActiveDirectory.GetUserInfoByUsername(result);
                        resultList.Add(result);
                        response = Request.CreateResponse(HttpStatusCode.OK, resultList);
                    }
                }
                else
                {
                    // No users entered.
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                string name = GetType().Name;
                response = Request.CreateResponse(HttpStatusCode.OK, $"Welcome to Wind API! - { name }");
            }

            return response;
        }

        // GET: api/ad/Users/[username]
        /// <summary>
        /// Gets a username and returns relevant user object info.
        /// </summary>
        /// <param name="user">A sAMAccountName to get info for</param>
        /// <returns>The input username and additional relevant AD fields.</returns>
        [HttpGet]
        [Route("{user}")]
        public HttpResponseMessage Get(string user)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (!string.IsNullOrEmpty(user))
            {
                UserInfoResult result = new UserInfoResult();
                result.sAMAccountName = user;
                result = ActiveDirectory.GetUserInfoByUsername(result);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }

            else
            {
                // No users entered.
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }

        // POST: api/ad/Users/IsMemberOf?group=[groupname]
        /// <summary>
        /// Gets a username and a group name and returnes whether the user is a member of the group.
        /// </summary>
        /// <param name="user">A Username to check group membership.</param>
        /// <param name="group">The group to check if the user is a member of.</param>
        /// <returns>An HttpResponseMessage containing a boolean specifying if the user is a
        /// member of the group or not.</returns>
        [HttpGet]
        [Route("{user}/IsMemberOf")]
        public HttpResponseMessage IsMemberOf(string user, [FromUri]string group)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            MembershipResult result = new MembershipResult();
            result.isMember = ActiveDirectory.IsUserMemberOf(user, group);

            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        // GET: api/ad/Users/[Username]/MemberOf
        /// <summary>
        /// Gets a username and returns all of the groups the user is a member of recursively.
        /// </summary>
        /// <param name="user">The username of the user in Active Directory.</param>
        /// <returns>An HttpResponseMessage containing a UserMembershipResult object with
        /// the input username and a list of groups in which the user is a member.</returns>
        [HttpGet]
        [Route("{user}/MemberOf")]
        public HttpResponseMessage UserGroupMembership(string user)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            UserMembershipResult result = new UserMembershipResult();
            result.user = user;
            result = ActiveDirectory.GetAllGroupsByUser(result);

            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}

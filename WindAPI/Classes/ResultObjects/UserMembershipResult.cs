using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the format that the UserGroupMembership action returns in the
    /// response body.
    /// </summary>
    public class UserMembershipResult
    {
        /// <summary>
        /// Internal status code for the Active Directory query.
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// Message explaining if the query succeeded or displays the error that occured.
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// The username to query group membership for.
        /// </summary>
        public string user { get; set; }
        /// <summary>
        /// A list of groups the username is a member of.
        /// </summary>
        public List<string> groups { get; set; }
    }
}
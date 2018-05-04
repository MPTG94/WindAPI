using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the result object returned by the GetGroupInfo action
    /// </summary>
    public class GroupInfoResult
    {
        /// <summary>
        /// Internal status code for the Active Directory query.
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// The groupname account name in AD
        /// </summary>
        public string sAMAccountName { get; set; }
        public string cn { get; set; }
        /// <summary>
        /// The display name of the group
        /// </summary>
        public string displayName { get; set; }
        /// <summary>
        /// The description of the group
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// The mail of the group
        /// </summary>
        public string mail { get; set; }
    }
}
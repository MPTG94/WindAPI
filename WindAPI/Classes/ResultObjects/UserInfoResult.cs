using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    public class UserInfoResult
    {
        /// <summary>
        /// Internal status code for the Active Directory query.
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// The username account name in AD
        /// </summary>
        public string sAMAccountName { get; set; }
        public string cn { get; set; }
        /// <summary>
        /// The display name of the user
        /// </summary>
        public string displayName { get; set; }
        /// <summary>
        /// The first name of the user
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// The last name of the user
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// The tite of the user (admin, manager...)
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// The mail of the user
        /// </summary>
        public string mail { get; set; }
        /// <summary>
        /// The phone number of the user
        /// </summary>
        public string telephoneNumber { get; set; }
        /// <summary>
        /// The voip of the user
        /// </summary>
        public string voip { get; set; }
        /// <summary>
        /// The full hierarchy of the user in the organization
        /// </summary>
        public string organizationHierarchy { get; set; }
    }
}
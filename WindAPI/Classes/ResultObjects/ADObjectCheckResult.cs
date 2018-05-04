using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the format that the CheckADObject action returns in the
    /// response body.
    /// </summary>
    public class ADObjectCheckResult
    {
        /// <summary>
        /// Internal status code for the Active Directory query.
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// The name of the AD object we checked.
        /// </summary>
        public string adObject { get; set; }
        /// <summary>
        /// The result specifying if the object is a user or group.
        /// </summary>
        public List<string> queryResult { get; set; }
    }
}
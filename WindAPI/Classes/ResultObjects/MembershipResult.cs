using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the format that the IsUserMember action returns in the
    /// response body.
    /// </summary>
    public class MembershipResult
    {
        /// <summary>
        /// Is the AD user a member of the input AD group
        /// </summary>
        public bool isMember { get; set; }
    }
}
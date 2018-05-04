using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the format that the CheckpointAPI auth request returns in the
    /// response body.
    /// </summary>
    public class CheckpointAuthResponse
    {
        /// <summary>
        /// The token provided for authentication by the user
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// The result if the user is an admin or not
        /// </summary>
        public bool isAdmin { get; set; }
        /// <summary>
        /// The username that the token belongs to
        /// </summary>
        public string name { get; set; }
    }
}
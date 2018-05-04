using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Config
{
    /// <summary>
    /// Class used for static configuration variables
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The default domain to query
        /// </summary>
        public static readonly string domain = "[DOMAIN]";
        /// <summary>
        /// The username of the user used to query AD
        /// </summary>
        public static readonly string adQueryUser = "[USER]";
        /// <summary>
        /// The password of the user used to query AD
        /// </summary>
        public static readonly string adQueryPassword = "[PASSWORD]";
        /// <summary>
        /// The Checkpoint API URL
        /// </summary>
        public static readonly string checkpointAPIURL = "[TOKEN_PROVIDER_URL]";
    }
}
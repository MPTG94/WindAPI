using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.RequestObjects
{
    /// <summary>
    /// Defines the request format that the CreateComputerAccount action accepts.
    /// </summary>
    public class ComputerAccountRequest
    {
        /// <summary>
        /// The name of the server to create a computer account for.
        /// </summary>
        public string serverName { get; set; }
        /// <summary>
        /// The environment of the server - production/test
        /// </summary>
        public string environment { get; set; }
        /// <summary>
        /// The name of the project to which the server belongs.
        /// </summary>
        public string projectName { get; set; }
        /// <summary>
        /// The name of the site in which the server is deployed.
        /// </summary>
        public string siteName { get; set; }
    }
}
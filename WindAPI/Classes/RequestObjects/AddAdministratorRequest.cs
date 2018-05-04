using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.RequestObjects
{
    /// <summary>
    /// Defines the request format that the AddAdmin action accepts.
    /// </summary>
    public class AddAdministratorRequest
    {
        /// <summary>
        /// The list of users to add as administrators
        /// </summary>
        public List<string> users { get; set; }
        /// The list of groups to add as administrators
        public List<string> groups { get; set; }
    }
}
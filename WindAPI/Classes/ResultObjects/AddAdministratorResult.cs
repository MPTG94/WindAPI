using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the format that the AddAdmin action returns in the
    /// response body.
    /// </summary>
    public class AddAdministratorResult
    {
        /// <summary>
        /// The name of the server administrators were added to.
        /// </summary>
        public string serverName { get; set; }
        /// <summary>
        /// The list of users that were added as administrators.
        /// </summary>
        public List<string> addedUsers { get; set; }
        /// <summary>
        /// The list of groups that were added as administrators.
        /// </summary>
        public List<string> addedGroups { get; set; }
        /// <summary>
        /// List of users that were already admins and weren't added again.
        /// </summary>
        public List<string> alreadyAdminUsers { get; set; }
        /// List of groups that were already admins and weren't added again.
        public List<string> alreadyAdminGroups { get; set; }
        /// <summary>
        /// A list of users that weren't added as administrators.
        /// </summary>
        public List<string> failedUsers { get; set; }
        /// <summary>
        /// A list of groups that weren't added as administrators.
        /// </summary>
        public List<string> failedGroups { get; set; }
        /// <summary>
        /// A result message.
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Creates a new instance of AddAdministratorResult with empty initialized lists.
        /// </summary>
        /// <param name="serverName">The name of the server to add administrators to.</param>
        public AddAdministratorResult(string serverName)
        {
            this.serverName = serverName;
            addedUsers = new List<string>();
            addedGroups = new List<string>();
            alreadyAdminUsers = new List<string>();
            alreadyAdminGroups = new List<string>();
            failedUsers = new List<string>();
            failedGroups = new List<string>();
            message = string.Empty;
        }
    }
}
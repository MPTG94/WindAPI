using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the result object returned by the Create/DeleteComputerAccount Action.
    /// </summary>
    public class ComputerAccountResult
    {
        /// <summary>
        /// The type of action to perform - create/delete.
        /// </summary>
        public string action { get; set; }
        /// <summary>
        /// Message containing a success message or error message.
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// The server name who's computer account was modified.
        /// </summary>
        public string serverName { get; set; }
        /// <summary>
        /// The path of the computer account in AD.
        /// </summary>
        public string objectADPath { get; set; }
    }
}
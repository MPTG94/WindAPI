using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.RequestObjects
{
    /// <summary>
    /// Defines the request format that the RunCommand action accepts.
    /// </summary>
    public class CommandRequest
    {
        /// <summary>
        /// The command to execute.
        /// </summary>
        public string commandToExecute { get; set; }
    }
}
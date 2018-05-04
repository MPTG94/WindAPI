using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindAPI.Classes.ResultObjects
{
    /// <summary>
    /// Defines the format that the RunCommand action returns in the
    /// response body.
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// The exit code of the command performed.
        /// </summary>
        public int commandExitCode { get; set; }
        /// <summary>
        /// The result message of the command performed.
        /// </summary>
        public string resultMessage { get; set; }
        /// <summary>
        /// The result error of the command performed.
        /// </summary>
        public string resultError { get; set; }
    }
}
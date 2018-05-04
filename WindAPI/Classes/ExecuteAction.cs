using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management.Automation;
using WindAPI.Classes.ResultObjects;
using System.IO;
using System.Diagnostics;

namespace WindAPI.Classes
{
    public static class ExecuteAction
    {
        public static string outputResult = string.Empty;
        public static string errorResult = string.Empty;
        public static int timeout = 240000;

        public static ExecutionResult ExecuteCommand(string serverName, string command)
        {
            // Variable to store powershell execution standard output
            outputResult = string.Empty;
            // Variable to store error in execution
            errorResult = string.Empty;
            // Variable to store the result of the execution
            ExecutionResult result = new ExecutionResult();

            try
            {
                PowerShell ps = PowerShell.Create();
                //ps.AddScript(string.Format("PsExec.exe \\\\{0} -h powershell.exe {1}", serverName, command));
                //ps.AddScript(string.Format("Invoke-Command -ComputerName {0} -ScriptBlock {{1}}", serverName, command));
                ps.AddScript("Invoke-Command -ComputerName " + serverName + " -ScriptBlock {" + command + "}");

                var results = ps.Invoke();

                if (results.Count > 0)
                {
                    result.commandExitCode = 0;
                    foreach (PSObject psObj in results)
                    {
                        if (psObj != null)
                        {
                            outputResult += psObj.ToString() + Environment.NewLine;
                        }
                    }
                    result.resultMessage = outputResult;
                }
            }
            catch (RuntimeException ex)
            {
                result.commandExitCode = -1;
                errorResult = ex.Message;
            }

            return result;
        }

        public static ExecutionResult ExecuteScript(string serverName, string scriptName)
        {
            // Variable to store powershell execution standard output
            outputResult = string.Empty;
            // Variable to store error in execution
            errorResult = string.Empty;
            // Variable to store path to the powershell scripts directory
            var path = string.Empty;
            // Variable to store the result of the execution
            ExecutionResult result = new ExecutionResult();

            try
            {
                PowerShell ps = PowerShell.Create();
                //ps.AddScript(string.Format("PsExec.exe \\\\{0} -h powershell.exe {1}", serverName, command));
                //ps.AddScript(string.Format("Invoke-Command -ComputerName {0} -ScriptBlock {{1}}", serverName, command));

                path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/PsScripts"),
                    scriptName);

                ps.AddScript("Invoke-Command -ComputerName " + serverName + " -FilePath " + path);

                var results = ps.Invoke();

                if (results.Count > 0)
                {
                    result.commandExitCode = 0;
                    foreach (PSObject psObj in results)
                    {
                        outputResult += psObj.ToString() + Environment.NewLine;
                    }
                    result.resultMessage = outputResult;
                }
            }
            catch (RuntimeException ex)
            {
                result.commandExitCode = -1;
                errorResult = ex.Message;
            }

            return result;
        }

        static void OutPutHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            outputResult += outLine.Data + Environment.NewLine;
        }
        static void ErrorHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            errorResult += outLine.Data + Environment.NewLine;
        }
    }
}
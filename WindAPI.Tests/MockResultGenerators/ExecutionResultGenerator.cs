using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;

namespace WindAPI.Tests.MockResultGenerators
{
    class ExecutionResultGenerator
    {
        public static ExecutionResult GenerateExecutionResultForValidTokenServerCommand()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 0;
            result.resultError = null;
            result.resultMessage = "exampleserver" + Environment.NewLine;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForValidTokenCommandInvalidServer()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 0;
            result.resultError = null;
            result.resultMessage = null;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForValidTokenServerInvalidCommand()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 0;
            result.resultError = null;
            result.resultMessage = null;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForValidTokenInvalidServerCommand()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 0;
            result.resultError = null;
            result.resultMessage = null;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForValidServerCommandInvalidToken()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 403;
            result.resultError = "Invalid token";
            result.resultMessage = null;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForValidCommandInvalidTokenServer()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 403;
            result.resultError = "Invalid token";
            result.resultMessage = null;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForValidServerInvalidTokenCommand()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 403;
            result.resultError = "Invalid token";
            result.resultMessage = null;

            return result;
        }

        public static ExecutionResult GenerateExecutionResultForInvalidTokenServerCommand()
        {
            ExecutionResult result = new ExecutionResult();
            result.commandExitCode = 403;
            result.resultError = "Invalid token";
            result.resultMessage = null;

            return result;
        }
    }
}

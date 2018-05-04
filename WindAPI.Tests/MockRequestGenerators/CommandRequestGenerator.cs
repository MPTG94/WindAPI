using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.RequestObjects;
using WindAPI.Tests.TestConfig;

namespace WindAPI.Tests.MockRequestGenerators
{
    class CommandRequestGenerator
    {
        public static CommandRequest GenerateValidCommandRequest()
        {
            CommandRequest request = new CommandRequest();
            request.commandToExecute = TestConstants.validCommand;

            return request;
        }

        public static CommandRequest GenerateValidCommandRequest_InvalidServer()
        {
            CommandRequest request = new CommandRequest();
            request.commandToExecute = TestConstants.validCommand;

            return request;
        }

        public static CommandRequest GenerateValidCommandRequest_InvalidCommand()
        {
            CommandRequest request = new CommandRequest();
            request.commandToExecute = TestConstants.invalidCommand;

            return request;
        }

        public static CommandRequest GenerateValidCommandRequest_InvalidServerAndCommand()
        {
            CommandRequest request = new CommandRequest();
            request.commandToExecute = TestConstants.invalidCommand;

            return request;
        }
    }
}

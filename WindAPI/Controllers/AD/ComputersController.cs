using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WindAPI.Classes;
using WindAPI.Classes.RequestObjects;
using WindAPI.Classes.ResultObjects;
using WindAPI.Handlers;

namespace WindAPI.Controllers.AD
{
    [RoutePrefix("api/ad/Computers")]
    public class ComputersController : ApiController
    {
        // GET: api/ad/Computers
        /// <summary>
        /// A basic GET request to the API to check if the API is alive.
        /// </summary>
        /// <returns>A welcome string.</returns>
        [Route("")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string name = GetType().Name;
            response = Request.CreateResponse(HttpStatusCode.OK, $"Welcome to Wind API! - { name }");

            return response;
        }

        // POST: api/ad/Computers?accessToken[CHECKPOINT_ACCESS_TOKEN]
        /// <summary>
        /// Gets the name of a server, environment, project and site and creates a computer account for it in AD
        /// </summary>
        /// <param name="accessToken">A checkpoint Access Token to check user permissions.</param>
        /// <param name="caRequest">A ComputerAccountRequest object containing the
        /// name of server, the environment, the project name and the site.</param>
        /// <returns>An HttpResponseMessage containing a ComputerAccountResult object with
        /// the name of the action performed, a success/failure message, the name of
        /// the server who's computer account was created and the path of the computer account
        /// in AD.</returns>
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromUri]string accessToken, [FromBody]ComputerAccountRequest caRequest)
        {
            bool isAuth = CheckpointAuth.CheckCheckpointToken(accessToken);

            HttpResponseMessage response = new HttpResponseMessage();

            ComputerAccountResult result = new ComputerAccountResult();

            if (isAuth)
            {
                if (caRequest.serverName.Length > 15)
                {
                    result.action = "create";
                    result.message = "Computer Account name longer than 15 characters.";
                    result.serverName = caRequest.serverName;

                    response = Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
                else
                {
                    result = ActiveDirectory.CreateComputerAccount(caRequest);

                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            else
            {
                result.action = "create";
                result.message = "Invalid token.";
                result.serverName = caRequest.serverName;

                response = Request.CreateResponse(HttpStatusCode.Forbidden, result);
            }


            return response;
        }

        // GET: api/ad/Computers/[computerName]/delete?accessToken=[CHECKPOINT_ACCESS_TOKEN]
        /// <summary>
        /// Gets the name of a server and deletes the corresponding
        /// computer account in AD.
        /// </summary>
        /// <param name="accessToken">A checkpoint Access Token to check user permissions.</param>
        /// <param name="computer">A string containing the name of a server.</param>
        /// <returns>An HttpResponseMessage containing a ComputerAccountResult object with
        /// the name of the action performed, a success/failure message, the name of
        /// the server who's computer account was deleted and the path of the computer account
        /// in AD.</returns>
        [HttpDelete]
        [Route("{computer}")]
        public HttpResponseMessage Delete(string computer, [FromUri]string accessToken)
        {
            bool isAuth = CheckpointAuth.CheckCheckpointToken(accessToken);

            HttpResponseMessage response = new HttpResponseMessage();

            ComputerAccountResult result = new ComputerAccountResult();

            if (isAuth)
            {
                result = ActiveDirectory.DeleteComputerAccount(computer);

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                result.action = "delete";
                result.message = "Invalid token.";
                result.serverName = computer;

                response = Request.CreateResponse(HttpStatusCode.Forbidden, result);
            }

            return response;
        }


        // POST: api/ad/computers/[computerName]/admins?accessToken=[CHECKPOINT_ACCESS_TOKEN]

        /// <summary>
        /// Adds administrator to a remote Windows Server.
        /// </summary>
        /// <param name="accessToken">A checkpoint Access Token to check user permissions.</param>
        /// <param name="computer">The name of the server to add administators to.</param>
        /// <param name="adminRequest">A AddAdministratorRequest object containing the users and groups
        /// to be added.</param>
        /// <returns>An HttpResponseMessage containing a AddAdministratorResult object which includes
        /// the users and groups that were added as admins, the users and groups that were already admins,
        /// the users and groups that weren't added and a result message.</returns>
        [HttpPost]
        [Route("{computer}/admins")]
        public HttpResponseMessage AddAdmin([FromUri]string accessToken, string computer,
            [FromBody]AddAdministratorRequest adminRequest)
        {
            bool isAuth = CheckpointAuth.CheckCheckpointToken(accessToken);

            HttpResponseMessage response = new HttpResponseMessage();

            if (isAuth)
            {
                AddAdministratorResult result =
                    ServerHandler.AddAdministratorsByRequestObject(computer, adminRequest);

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                AddAdministratorResult result = new AddAdministratorResult(computer);
                result.message = "Invalid Token";

                response = Request.CreateResponse(HttpStatusCode.Forbidden, result);
            }

            return response;
        }

        // POST: api/ad/computers/[computerName]/exec?accessToken=[CHECKPOINT_ACCESS_TOKEN]
        /// <summary>
        /// runs a command on a remote Windows server.
        /// </summary>
        /// <param name="computer">The name of the server to execut command on</param>
        /// <param name="accessToken">A checkpoint Access Token to check user permissions.</param>
        /// <param name="command">A CommandRequest object</param>
        /// <returns>An HttpResponseMessage containing an ExecutionResult object which includes:
        /// commandExitCode - The exit code of the command performed.
        /// resultMessage - The output of the command run on the remote server.
        /// resultError - The error output of the execution on the remote sever.</returns>
        [HttpPost]
        [Route("{computer}/exec")]
        public HttpResponseMessage RunCommand(string computer, [FromUri]string accessToken, [FromBody]CommandRequest command)
        {
            bool isAuth = CheckpointAuth.CheckCheckpointToken(accessToken);

            HttpResponseMessage response = new HttpResponseMessage();

            if (isAuth)
            {
                ExecutionResult result = new ExecutionResult();

                result = ExecuteAction.ExecuteCommand(computer,
                    command.commandToExecute);

                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                ExecutionResult result = new ExecutionResult();

                result.commandExitCode = 403;
                result.resultError = "Invalid token";

                response = Request.CreateResponse(HttpStatusCode.Forbidden, result);
            }

            return response;
        }


    }
}

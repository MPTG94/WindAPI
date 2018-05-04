using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindAPI.Classes.ResultObjects;
using WindAPI.Tests.TestConfig;
using WindAPI.Tests.MockResultGenerators;
using WindAPI.Classes.RequestObjects;
using WindAPI.Controllers.AD;
using WindAPI.Tests.Handlers;
using System.Net.Http;
using WindAPI.Handlers;
using WindAPI.Tests.MockRequestGenerators;

namespace WindAPI.Tests.Controllers.AD
{
    /// <summary>
    /// Test class for all methods in the ComputersController.
    /// </summary>
    [TestClass]
    public class ComputersControllerTest
    {
        #region BasicGet_Tests

        [TestMethod]
        public void Get_ShouldReturnWelcomeMessage()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = "Welcome to Wind API! - ComputersController";

            // Act.
            var result = controller.Get();

            // Assert.
            Assert.IsNotNull(result);
            var getResult = result.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, getResult);
        }

        #endregion

        #region Post_Tests

        [TestMethod]
        public void Post_NewCAInNewOUTest_ShouldReturnSuccessAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountTest;
            string environment = TestConstants.TestEnvironment;
            string projectName = TestConstants.NewOrganizationalUnitTest;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForNewCAInTest(true, siteName);

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Cleanup.
            ActiveDirectory.DeleteOrganizationalUnitTreeVoid(environment, projectName);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_NewCAInNewOUProd_ShouldReturnSuccessAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountProd;
            string environment = TestConstants.ProductionEnvironment;
            string projectName = TestConstants.NewOrganizationalUnitProd;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForNewCAInProd(true, siteName);

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Cleanup.
            ActiveDirectory.DeleteOrganizationalUnitTreeVoid(environment, projectName);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_NewCAInExistingOUTest_ShouldReturnSuccessAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountTest;
            string environment = TestConstants.TestEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitTest;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForNewCAInTest(false, siteName);

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Cleanup.
            ActiveDirectory.DeleteComputerAccountVoid(serverName);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_NewCAInExistingOUProd_ShouldReturnSuccessAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountProd;
            string environment = TestConstants.ProductionEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitProd;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForNewCAInProd(false, siteName);

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Cleanup.
            ActiveDirectory.DeleteComputerAccountVoid(serverName);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_ExistingCATest_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.ExistingComputerAccountTest;
            string environment = TestConstants.TestEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitTest;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForExistingCAInTest(siteName);

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_ExistingCAProd_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.ExistingComputerAccountProd;
            string environment = TestConstants.ProductionEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitProd;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForExistingCAInProd(siteName);

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_LongCANameTest_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.TooLongComputerAccountNameTest;
            string environment = TestConstants.TestEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitTest;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForTooLongCANameTest();

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_LongCANameProd_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.TooLongComputerAccountNameProd;
            string environment = TestConstants.ProductionEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitProd;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForTooLongCANameProd();

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_InvalidSiteTest_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountTest;
            string environment = TestConstants.TestEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitTest;
            string siteName = TestConstants.InvalidSite;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForInvalidSiteTest();

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_InvalidSiteProd_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountProd;
            string environment = TestConstants.ProductionEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitProd;
            string siteName = TestConstants.InvalidSite;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForInvalidSiteProd();

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_InvalidEnvironmentTest_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountTest;
            string environment = TestConstants.InvalidEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitTest;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForInvalidEnvironmentTest();

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Post_InvalidEnvironmentProd_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NewComputerAccountProd;
            string environment = TestConstants.InvalidEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitProd;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForInvalidEnvironmentProd();

            // Act.
            var result = controller.Post(TestConstants.checkpointAdminAcccessToken, caRequest);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        #endregion

        #region Delete_Tests

        [TestMethod]
        public void Delete_NonExistingCATest_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NonExistingComputerAccountToDeleteTest;
            string environment = TestConstants.TestEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitTest;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForNonExistingCADeleteTest(siteName);

            // Act.
            var result =
                controller.Delete(serverName, TestConstants.checkpointAdminAcccessToken);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        [TestMethod]
        public void Delete_NonExistingCAProd_ShouldReturnErrorAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();

            string serverName = TestConstants.NonExistingComputerAccountToDeleteProd;
            string environment = TestConstants.ProductionEnvironment;
            string projectName = TestConstants.ExistingOrganizationalUnitProd;
            string siteName = TestConstants.SitePrimary;

            var caRequest = new ComputerAccountRequest()
            {
                serverName = serverName,
                environment = environment,
                projectName = projectName,
                siteName = siteName
            };

            var expected = ComputerAccountResultGenerator.GenerateResultForNonExistingCADeleteProd(siteName);

            // Act.
            var result =
                controller.Delete(serverName, TestConstants.checkpointAdminAcccessToken);

            // Assert.
            Assert.IsNotNull(result);
            var caResult = result.Content.ReadAsAsync<ComputerAccountResult>().Result;
            Assert.AreEqual(expected.action, caResult.action);
            Assert.AreEqual(expected.message, caResult.message);
            Assert.AreEqual(expected.objectADPath, caResult.objectADPath);
            Assert.AreEqual(expected.serverName, caResult.serverName);
        }

        #endregion

        #region RunCommand_Tests

        [TestMethod]
        public void RunCommand_ValidTokenServerCommand_ShouldReturnSuccessAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidTokenServerCommand();

            // Act.
            var result = controller.RunCommand(TestConstants.validServerName, TestConstants.checkpointAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_ValidTokenServerAndInvalidCommand_ShouldReturnErrorMessageAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidTokenServerInvalidCommand();

            // Act.
            var result = controller.RunCommand(TestConstants.validServerName, TestConstants.checkpointAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest_InvalidCommand());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_ValidTokenCommandAndInvalidServer_ShouldReturnErrorMessageAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidTokenCommandInvalidServer();

            // Act.
            var result = controller.RunCommand(TestConstants.invalidServerName, TestConstants.checkpointAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest_InvalidServer());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_ValidTokenAndInvalidServerCommand_ShouldReturnErrorMessageAndResult()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidTokenInvalidServerCommand();

            // Act.
            var result = controller.RunCommand(TestConstants.invalidServerName, TestConstants.checkpointAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest_InvalidServerAndCommand());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_ValidServerCommandAndInvalidToken_ShouldReturnForbidden()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidServerCommandInvalidToken();

            // Act.
            var result = controller.RunCommand(TestConstants.validServerName, TestConstants.checkpointNonAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_ValidCommandAndInvalidTokenServer_ShouldReturnForbidden()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidCommandInvalidTokenServer();

            // Act.
            var result = controller.RunCommand(TestConstants.invalidServerName, TestConstants.checkpointNonAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest_InvalidServer());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_ValidServerAndInvalidTokenCommand_ShouldReturnForbidden()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForValidServerInvalidTokenCommand();

            // Act.
            var result = controller.RunCommand(TestConstants.validServerName, TestConstants.checkpointNonAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest_InvalidCommand());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        [TestMethod]
        public void RunCommand_InvalidTokenServerCommand_ShouldReturnForbidden()
        {
            // Arrange.
            var controller = new ComputersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = ExecutionResultGenerator.GenerateExecutionResultForInvalidTokenServerCommand();

            // Act.
            var result = controller.RunCommand(TestConstants.invalidServerName, TestConstants.checkpointNonAdminAcccessToken, CommandRequestGenerator.GenerateValidCommandRequest_InvalidServerAndCommand());

            // Assert.
            Assert.IsNotNull(result);
            var executionResult = result.Content.ReadAsAsync<ExecutionResult>().Result;
            Assert.AreEqual(expected.commandExitCode, executionResult.commandExitCode);
            Assert.AreEqual(expected.resultMessage, executionResult.resultMessage);
            Assert.AreEqual(expected.resultError, executionResult.resultError);
        }

        #endregion
    }
}

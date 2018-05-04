using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindAPI.Controllers.AD;
using WindAPI.Tests.Handlers;
using WindAPI.Tests.TestConfig;
using WindAPI.Tests.MockResultGenerators;
using WindAPI.Classes.ResultObjects;
using System.Net.Http;

namespace WindAPI.Tests.Controllers.AD
{
    /// <summary>
    /// Test class for all methods in the ObjectsController.
    /// </summary>
    [TestClass]
    public class ObjectsControllerTest
    {
        #region BasicGet_Tests

        [TestMethod]
        public void Get_ShouldReturnWelcomeMessage()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = "Welcome to Wind API! - ObjectsController";

            // Act.
            var result = controller.Get();

            // Assert.
            Assert.IsNotNull(result);
            var getResult = result.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, getResult);
        }

        #endregion

        #region CheckADObject_Tests

        [TestMethod]
        public void CheckADObject_ValidUser_ShouldReturnCodeAndQueryResult()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var adObject = TestConstants.UserPrimary;
            var expected = ADObjectCheckResultGenerator.GenerateADObjectCheckResultForValidUser();

            // Act.
            var result = controller.CheckADObject(adObject);

            // Assert.
            Assert.IsNotNull(result);
            var adObjectCheckResult = result.Content.ReadAsAsync<ADObjectCheckResult>().Result;
            Assert.IsTrue(adObjectCheckResult.code == 0);
            Assert.AreEqual(expected.code, adObjectCheckResult.code);
            Assert.AreEqual(expected.adObject, adObjectCheckResult.adObject);
            for (int i = 0; i < adObjectCheckResult.queryResult.Count; i++)
            {
                Assert.AreEqual(expected.queryResult[i], adObjectCheckResult.queryResult[i]);
            }
        }

        [TestMethod]
        public void CheckADObject_ValidGroup_ShouldReturnCodeAndQueryResult()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var adObject = TestConstants.GroupPrime;
            var expected = ADObjectCheckResultGenerator.GenerateADObjectCheckResultForValidGroup();

            // Act.
            var result = controller.CheckADObject(adObject);

            // Assert.
            Assert.IsNotNull(result);
            var adObjectCheckResult = result.Content.ReadAsAsync<ADObjectCheckResult>().Result;
            Assert.IsTrue(adObjectCheckResult.code == 1);
            Assert.AreEqual(expected.code, adObjectCheckResult.code);
            Assert.AreEqual(expected.adObject, adObjectCheckResult.adObject);
            for (int i = 0; i < adObjectCheckResult.queryResult.Count; i++)
            {
                Assert.AreEqual(expected.queryResult[i], adObjectCheckResult.queryResult[i]);
            }
        }

        [TestMethod]
        public void CheckADObject_ValidComputer_ShouldReturnCodeAndQueryResult()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var adObject = TestConstants.ComputerPrimary;
            var expected = ADObjectCheckResultGenerator.GenerateADObjectCheckResultForValidComputer();

            // Act.
            var result = controller.CheckADObject(adObject);

            // Assert.
            Assert.IsNotNull(result);
            var adObjectCheckResult = result.Content.ReadAsAsync<ADObjectCheckResult>().Result;
            Assert.IsTrue(adObjectCheckResult.code == 2);
            Assert.AreEqual(expected.code, adObjectCheckResult.code);
            Assert.AreEqual(expected.adObject, adObjectCheckResult.adObject);
            for (int i = 0; i < adObjectCheckResult.queryResult.Count; i++)
            {
                Assert.AreEqual(expected.queryResult[i], adObjectCheckResult.queryResult[i]);
            }
        }

        [TestMethod]
        public void CheckADObject_ValidUserAndComputer_ShouldReturnCodeAndQueryResult()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var adObject = TestConstants.UserAndComputer;
            var expected = ADObjectCheckResultGenerator.GenerateADObjectCheckResultForValidUserAndComputer();

            // Act.
            var result = controller.CheckADObject(adObject);

            // Assert.
            Assert.IsNotNull(result);
            var adObjectCheckResult = result.Content.ReadAsAsync<ADObjectCheckResult>().Result;
            Assert.IsTrue(adObjectCheckResult.code == 3);
            Assert.AreEqual(expected.code, adObjectCheckResult.code);
            Assert.AreEqual(expected.adObject, adObjectCheckResult.adObject);
            for (int i = 0; i < adObjectCheckResult.queryResult.Count; i++)
            {
                Assert.AreEqual(expected.queryResult[i], adObjectCheckResult.queryResult[i]);
            }
        }

        [TestMethod]
        public void CheckADObject_ValidGroupAndComputer_ShouldReturnCodeAndQueryResult()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var adObject = TestConstants.GroupAndComputer;
            var expected = ADObjectCheckResultGenerator.GenerateADObjectCheckResultForValidGroupAndComputer();

            // Act.
            var result = controller.CheckADObject(adObject);

            // Assert.
            Assert.IsNotNull(result);
            var adObjectCheckResult = result.Content.ReadAsAsync<ADObjectCheckResult>().Result;
            Assert.IsTrue(adObjectCheckResult.code == 4);
            Assert.AreEqual(expected.code, adObjectCheckResult.code);
            Assert.AreEqual(expected.adObject, adObjectCheckResult.adObject);
            for (int i = 0; i < adObjectCheckResult.queryResult.Count; i++)
            {
                Assert.AreEqual(expected.queryResult[i], adObjectCheckResult.queryResult[i]);
            }
        }

        [TestMethod]
        public void CheckADObject_NonExistingADObject_ShouldReturnCodeAndErrorMessage()
        {
            // Arrange.
            var controller = new ObjectsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var adObject = TestConstants.InvalidADObject;
            var expected = ADObjectCheckResultGenerator.GenerateADObjectCheckResultForNonExistingADObject();

            // Act.
            var result = controller.CheckADObject(adObject);

            // Assert.
            Assert.IsNotNull(result);
            var adObjectCheckResult = result.Content.ReadAsAsync<ADObjectCheckResult>().Result;
            Assert.IsTrue(adObjectCheckResult.code == 5);
            Assert.AreEqual(expected.code, adObjectCheckResult.code);
            Assert.AreEqual(expected.adObject, adObjectCheckResult.adObject);
            for (int i = 0; i < adObjectCheckResult.queryResult.Count; i++)
            {
                Assert.AreEqual(expected.queryResult[i], adObjectCheckResult.queryResult[i]);
            }
        }

        #endregion
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindAPI.Controllers.AD;
using WindAPI.Tests.TestConfig;
using WindAPI.Tests.Handlers;
using WindAPI.Tests.MockResultGenerators;
using WindAPI.Classes.ResultObjects;
using System.Collections.Generic;
using System.Net.Http;

namespace WindAPI.Tests.Controllers.AD
{
    /// <summary>
    /// Test class for all methods in the GroupsController.
    /// </summary>
    [TestClass]
    public class GroupsControllerTest
    {
        #region BasicGet_Tests

        [TestMethod]
        public void Get_ShouldReturnWelcomeMessage()
        {
            // Arrange.
            var controller = new GroupsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = "Welcome to Wind API! - GroupsController";

            // Act.
            var result = controller.Get();

            // Assert.
            Assert.IsNotNull(result);
            var getResult = result.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, getResult);
        }

        [TestMethod]
        public void GetGroupInfo_MultipleValidGroups_ShouldReturnSuccessAndList()
        {
            // Arrange.
            var controller = new GroupsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var group = TestConstants.GroupListMulti;
            var expected = GroupInfoResultGenerator.GenerateGroupInfoResultForMultipleValidGroups();

            // Act.
            var result = controller.Get(group);

            // Assert.
            Assert.IsNotNull(result);
            var groupInfoResult = result.Content.ReadAsAsync<List<GroupInfoResult>>().Result;
            Assert.IsTrue(groupInfoResult.Count > 0);
            Assert.IsTrue(expected.Count == groupInfoResult.Count);
            for (int i = 0; i < groupInfoResult.Count; i++)
            {
                Assert.IsTrue(groupInfoResult[i].code == 0);
                Assert.AreEqual(expected[i].sAMAccountName, groupInfoResult[i].sAMAccountName);
                Assert.AreEqual(expected[i].code, groupInfoResult[i].code);
                Assert.AreEqual(expected[i].displayName, groupInfoResult[i].displayName);
                Assert.AreEqual(expected[i].description, groupInfoResult[i].description);
            }
        }

        [TestMethod]
        public void GetGroupInfo_MultipleInvalidGroups_ShouldReturnErrorCodeAndList()
        {
            // Arrange.
            var controller = new GroupsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var group = TestConstants.InvalidGroupListMulti;
            var expected = GroupInfoResultGenerator.GenerateGroupInfoResultForMultipleInvalidGroups();

            // Act.
            var result = controller.Get(group);

            // Assert.
            Assert.IsNotNull(result);
            var groupInfoResult = result.Content.ReadAsAsync<List<GroupInfoResult>>().Result;
            Assert.IsTrue(groupInfoResult.Count > 0);
            Assert.IsTrue(expected.Count == groupInfoResult.Count);
            for (int i = 0; i < groupInfoResult.Count; i++)
            {
                Assert.IsTrue(groupInfoResult[i].code == 1);
                Assert.AreEqual(expected[i].sAMAccountName, groupInfoResult[i].sAMAccountName);
                Assert.AreEqual(expected[i].code, groupInfoResult[i].code);
                Assert.AreEqual(expected[i].displayName, groupInfoResult[i].displayName);
                Assert.AreEqual(expected[i].description, groupInfoResult[i].description);
            }
        }

        #endregion

        #region GetSingleGroupInfo_Tests

        [TestMethod]
        public void GetGroupInfo_SingleValidGroup_ShouldReturnSuccessAndList()
        {
            // Arrange.
            var controller = new GroupsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = GroupInfoResultGenerator.GenerateGroupInfoResultForSingleValidGroup();

            // Act.
            var result = controller.Get(TestConstants.GroupInfoPrime);

            // Assert.
            Assert.IsNotNull(result);
            var groupInfoResult = result.Content.ReadAsAsync<GroupInfoResult>().Result;
            Assert.IsTrue(groupInfoResult.code == 0);
            Assert.AreEqual(expected.sAMAccountName, groupInfoResult.sAMAccountName);
            Assert.AreEqual(expected.code, groupInfoResult.code);
            Assert.AreEqual(expected.displayName, groupInfoResult.displayName);
            Assert.AreEqual(expected.description, groupInfoResult.description);
        }

        [TestMethod]
        public void GetUGroupInfo_SingleInvalidGroup_ShouldReturnErrorCodeAndList()
        {
            // Arrange.
            var controller = new GroupsController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = GroupInfoResultGenerator.GenerateGroupInfoResultForSingleInvalidGroup();

            // Act.
            var result = controller.Get(TestConstants.GroupInvalidPrime);

            // Assert.
            Assert.IsNotNull(result);
            var groupInfoResult = result.Content.ReadAsAsync<GroupInfoResult>().Result;
            Assert.IsTrue(groupInfoResult.code == 1);
            Assert.AreEqual(expected.sAMAccountName, groupInfoResult.sAMAccountName);
            Assert.AreEqual(expected.code, groupInfoResult.code);
            Assert.AreEqual(expected.displayName, groupInfoResult.displayName);
            Assert.AreEqual(expected.description, groupInfoResult.description);

        }

        #endregion
    }
}

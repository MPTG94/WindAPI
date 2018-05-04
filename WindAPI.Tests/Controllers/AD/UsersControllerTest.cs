using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using WindAPI.Classes.ResultObjects;
using WindAPI.Tests.MockResultGenerators;
using WindAPI.Tests.TestConfig;
using System.Collections.Generic;
using WindAPI.Classes.RequestObjects;
using WindAPI.Handlers;
using WindAPI.Tests.Handlers;
using WindAPI.Controllers.AD;

namespace WindAPI.Tests.Controllers.AD
{
    /// <summary>
    /// Test class for all methods in the UsersController.
    /// </summary>
    [TestClass]
    public class UsersControllerTest
    {
        #region BasicGet_Tests

        [TestMethod]
        public void Get_ShouldReturnWelcomeMessage()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = "Welcome to Wind API! - UsersController";

            // Act.
            var result = controller.Get();

            // Assert.
            Assert.IsNotNull(result);
            var getResult = result.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, getResult);
        }

        [TestMethod]
        public void Get_MultipleValidUsers_ShouldReturnSuccessAndList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var user = TestConstants.UserListMulti;
            var expected = UserInfoResultGenerator.GenerateUserInfoResultForMultipleValidUsers();

            // Act.
            var result = controller.Get(user);

            // Assert.
            Assert.IsNotNull(result);
            var userInfoResult = result.Content.ReadAsAsync<List<UserInfoResult>>().Result;
            Assert.IsTrue(userInfoResult.Count > 0);
            Assert.IsTrue(expected.Count == userInfoResult.Count);
            for (int i = 0; i < userInfoResult.Count; i++)
            {
                Assert.IsTrue(userInfoResult[i].code == 0);
                Assert.AreEqual(expected[i].sAMAccountName, userInfoResult[i].sAMAccountName);
                Assert.AreEqual(expected[i].code, userInfoResult[i].code);
                Assert.AreEqual(expected[i].displayName, userInfoResult[i].displayName);
            }
        }

        [TestMethod]
        public void Get_MultipleInvalidUsers_ShouldReturnErrorCodeAndList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = UserInfoResultGenerator.GenerateUserInfoResultForMultipleInvalidUsers();

            // Act.
            var result = controller.Get(TestConstants.InvalidUserListMulti);

            // Assert.
            Assert.IsNotNull(result);
            var userInfoResult = result.Content.ReadAsAsync<List<UserInfoResult>>().Result;
            Assert.IsTrue(userInfoResult.Count > 0);
            Assert.IsTrue(expected.Count == userInfoResult.Count);
            for (int i = 0; i < userInfoResult.Count; i++)
            {
                Assert.IsTrue(userInfoResult[i].code == 1);
                Assert.AreEqual(expected[i].sAMAccountName, userInfoResult[i].sAMAccountName);
                Assert.AreEqual(expected[i].code, userInfoResult[i].code);
                Assert.AreEqual(expected[i].displayName, userInfoResult[i].displayName);
            }
        }

        #endregion

        #region GetUserInfo_Tests

        [TestMethod]
        public void GetUserInfo_SingleValidUser_ShouldReturnSuccessAndList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = UserInfoResultGenerator.GenerateUserInfoResultForSingleValidUser();

            // Act.
            var result = controller.Get(TestConstants.UserPrimary);

            // Assert.
            Assert.IsNotNull(result);
            var userInfoResult = result.Content.ReadAsAsync<UserInfoResult>().Result;
            Assert.IsTrue(userInfoResult.code == 0);
            Assert.AreEqual(expected.sAMAccountName, userInfoResult.sAMAccountName);
            Assert.AreEqual(expected.code, userInfoResult.code);
            Assert.AreEqual(expected.displayName, userInfoResult.displayName);
        }

        [TestMethod]
        public void GetUserInfo_SingleInvalidUser_ShouldReturnErrorCodeAndList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = UserInfoResultGenerator.GenerateUserInfoResultForSingleInvalidUser();

            // Act.
            var result = controller.Get(TestConstants.UserInvalidPrimary);

            // Assert.
            Assert.IsNotNull(result);
            var userInfoResult = result.Content.ReadAsAsync<UserInfoResult>().Result;
            Assert.IsTrue(userInfoResult.code == 1);
            Assert.AreEqual(expected.sAMAccountName, userInfoResult.sAMAccountName);
            Assert.AreEqual(expected.code, userInfoResult.code);
            Assert.AreEqual(expected.displayName, userInfoResult.displayName);
        }

        #endregion

        #region IsUserMember_Tests

        [TestMethod]
        public void IsUserMember_ExistingGroupAndUserInGroup_ShouldReturnTrue()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expcted = MembershipResultGenerator.GenerateMembershipResultForTrueResult();

            // Act.
            var result = controller.IsMemberOf(TestConstants.UserSecondary, TestConstants.GroupPrime);

            // Assert.
            Assert.IsNotNull(result);
            var membershipResult = result.Content.ReadAsAsync<MembershipResult>().Result;
            Assert.AreEqual(expcted.isMember, membershipResult.isMember);
        }

        [TestMethod]
        public void IsUserMember_ExistingGroupAndUserNotInGroup_ShouldReturnFalse()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expcted = MembershipResultGenerator.GenerateMembershipResultForFalseResult();

            // Act.
            var result = controller.IsMemberOf(TestConstants.UserPrimary, TestConstants.GroupNoMembership);

            // Assert.
            Assert.IsNotNull(result);
            var membershipResult = result.Content.ReadAsAsync<MembershipResult>().Result;
            Assert.AreEqual(expcted.isMember, membershipResult.isMember);
        }

        [TestMethod]
        public void IsUserMember_ExistingUserAndNonExistingGroup_ShouldReturnFalse()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expcted = MembershipResultGenerator.GenerateMembershipResultForFalseResult();

            // Act.
            var result = controller.IsMemberOf(TestConstants.UserPrimary, TestConstants.GroupInvalidPrime);

            // Assert.
            Assert.IsNotNull(result);
            var membershipResult = result.Content.ReadAsAsync<MembershipResult>().Result;
            Assert.AreEqual(expcted.isMember, membershipResult.isMember);
        }

        [TestMethod]
        public void IsUserMember_NonExistingUserAndExistingGroup_ShouldReturnFalse()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expcted = MembershipResultGenerator.GenerateMembershipResultForFalseResult();

            // Act.
            var result = controller.IsMemberOf(TestConstants.UserInvalidPrimary, TestConstants.GroupPrime);

            // Assert.
            Assert.IsNotNull(result);
            var membershipResult = result.Content.ReadAsAsync<MembershipResult>().Result;
            Assert.AreEqual(expcted.isMember, membershipResult.isMember);
        }

        [TestMethod]
        public void IsUserMember_NonExistingUserAndNonExistingGroup_ShouldReturnFalse()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expcted = MembershipResultGenerator.GenerateMembershipResultForFalseResult();

            // Act.
            var result = controller.IsMemberOf(TestConstants.UserInvalidSecondary, TestConstants.GroupInvalidPrime);

            // Assert.
            Assert.IsNotNull(result);
            var membershipResult = result.Content.ReadAsAsync<MembershipResult>().Result;
            Assert.AreEqual(expcted.isMember, membershipResult.isMember);
        }

        #endregion

        #region UserGroupMembership_Tests

        [TestMethod]
        public void UserGroupMembership_ValidUser_ShouldReturnSuccessAndList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = UserMembershipResultGenerator.GenerateMembershipResultForValidUser();

            // Act.
            var result = controller.UserGroupMembership(TestConstants.UserPrimary);

            // Assert.
            Assert.IsNotNull(result);
            var userMembershipResult = result.Content.ReadAsAsync<UserMembershipResult>().Result;
            Assert.AreEqual(expected.user, userMembershipResult.user);
            Assert.AreEqual(expected.code, userMembershipResult.code);
            Assert.AreEqual(expected.message, userMembershipResult.message);
            Assert.IsTrue(userMembershipResult.groups.Count > 0);
        }

        [TestMethod]
        public void UserGroupMembership_ValidUserWithNoMatchingException_ShouldReturnErrorMessageAndList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = UserMembershipResultGenerator.GenerateMembershipResultForValidUserWithNoMatchingException();

            // Act.
            var result = controller.UserGroupMembership(TestConstants.UserNoMatching);

            // Assert.
            Assert.IsNotNull(result);
            var userMembershipResult = result.Content.ReadAsAsync<UserMembershipResult>().Result;
            Assert.AreEqual(expected.user, userMembershipResult.user);
            Assert.AreEqual(expected.code, userMembershipResult.code);
            Assert.AreEqual(expected.message, userMembershipResult.message);
            Assert.IsTrue(userMembershipResult.groups.Count > 0);
        }

        [TestMethod]
        public void UserGroupMembership_InvalidUser_ShouldReturnErrorMessageAndEmptyList()
        {
            // Arrange.
            var controller = new UsersController();
            controller.Request = HttpRequestHandler.GenerateHttpRequestMessage();
            var expected = UserMembershipResultGenerator.GenerateMembershipResultForInvalidUser();

            // Act.
            var result = controller.UserGroupMembership(TestConstants.UserInvalidPrimary);

            // Assert.
            Assert.IsNotNull(result);
            var userMembershipResult = result.Content.ReadAsAsync<UserMembershipResult>().Result;
            Assert.AreEqual(expected.user, userMembershipResult.user);
            Assert.AreEqual(expected.code, userMembershipResult.code);
            Assert.AreEqual(expected.message, userMembershipResult.message);
            Assert.IsTrue(userMembershipResult.groups.Count == 0);
        }

        #endregion
    }
}

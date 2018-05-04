using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;
using WindAPI.Tests.TestConfig;

namespace WindAPI.Tests.MockResultGenerators
{
    class UserInfoResultGenerator
    {
        public static UserInfoResult GenerateUserInfoResultForSingleValidUser()
        {
            UserInfoResult result = new UserInfoResult()
            {
                sAMAccountName = TestConstants.UserPrimary,
                code = 0,
                displayName = "Primary Test User"
            };

            return result;
        }

        public static List<UserInfoResult> GenerateUserInfoResultForMultipleValidUsers()
        {
            List<UserInfoResult> result = new List<UserInfoResult>();
            result.Add(new UserInfoResult()
            {
                sAMAccountName = TestConstants.UserPrimary,
                code = 0,
                displayName = "Primary Test User"
            });

            result.Add(new UserInfoResult()
            {
                sAMAccountName = TestConstants.UserSecondary,
                code = 0,
                displayName = "Secondary Test User"
            });

            return result;
        }

        public static UserInfoResult GenerateUserInfoResultForSingleInvalidUser()
        {
            UserInfoResult result = new UserInfoResult()
            {
                sAMAccountName = TestConstants.UserInvalidPrimary,
                code = 1,
                displayName = null
            };

            return result;
        }

        public static List<UserInfoResult> GenerateUserInfoResultForMultipleInvalidUsers()
        {
            List<UserInfoResult> result = new List<UserInfoResult>();
            result.Add(new UserInfoResult()
            {
                sAMAccountName = TestConstants.UserInvalidPrimary,
                code = 1,
                displayName = null
            });

            result.Add(new UserInfoResult()
            {
                sAMAccountName = TestConstants.UserInvalidSecondary,
                code = 1,
                displayName = null
            });

            return result;
        }
    }
}

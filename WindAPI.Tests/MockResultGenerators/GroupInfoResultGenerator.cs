using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;
using WindAPI.Tests.TestConfig;

namespace WindAPI.Tests.MockResultGenerators
{
    class GroupInfoResultGenerator
    {
        public static GroupInfoResult GenerateGroupInfoResultForSingleValidGroup()
        {
            GroupInfoResult result = new GroupInfoResult()
            {
                sAMAccountName = TestConstants.GroupInfoPrime,
                code = 0,
                displayName = null,
                description = "Designated administrators of the domain"
            };

            return result;
        }

        public static List<GroupInfoResult> GenerateGroupInfoResultForMultipleValidGroups()
        {
            List<GroupInfoResult> result = new List<GroupInfoResult>();
            result.Add(new GroupInfoResult()
            {
                sAMAccountName = TestConstants.GroupInfoPrime,
                code = 0,
                displayName = null,
                description = "Designated administrators of the domain"
            });

            result.Add(new GroupInfoResult()
            {
                sAMAccountName = TestConstants.GroupInfoSecond,
                code = 0,
                displayName = null,
                description = "Users are prevented from making accidental or intentional system-wide changes.  Thus, Users can run certified applications, but not most legacy applications"
            });
            return result;
        }

        public static GroupInfoResult GenerateGroupInfoResultForSingleInvalidGroup()
        {
            GroupInfoResult result = new GroupInfoResult()
            {
                sAMAccountName = TestConstants.GroupInvalidPrime,
                code = 1,
                displayName = null,
                description = null
            };

            return result;
        }

        public static List<GroupInfoResult> GenerateGroupInfoResultForMultipleInvalidGroups()
        {
            List<GroupInfoResult> result = new List<GroupInfoResult>();
            result.Add(new GroupInfoResult()
            {
                sAMAccountName = TestConstants.GroupInvalidPrime,
                code = 1,
                displayName = null,
                description = null
            });

            result.Add(new GroupInfoResult()
            {
                sAMAccountName = TestConstants.GroupInvalidSecond,
                code = 1,
                displayName = null,
                description = null
            });

            return result;
        }
    }
}

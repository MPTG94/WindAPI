using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;
using WindAPI.Tests.TestConfig;

namespace WindAPI.Tests.MockResultGenerators
{
    class UserMembershipResultGenerator
    {
        public static UserMembershipResult GenerateMembershipResultForValidUser()
        {
            UserMembershipResult result = new UserMembershipResult();
            result.user = TestConstants.UserPrimary;
            result.code = 0;
            result.message = "Group list generated successfully";

            return result;
        }

        public static UserMembershipResult GenerateMembershipResultForValidUserWithNoMatchingException()
        {
            UserMembershipResult result = new UserMembershipResult();
            result.user = TestConstants.UserNoMatching;
            result.code = 1;
            result.message = "An error occurred while enumerating the groups.  The group could not be found.";

            return result;
        }

        public static UserMembershipResult GenerateMembershipResultForInvalidUser()
        {
            UserMembershipResult result = new UserMembershipResult();
            result.user = TestConstants.UserInvalidPrimary;
            result.code = 2;
            result.message = "Input user not found in Active Directory";

            return result;
        }
    }
}

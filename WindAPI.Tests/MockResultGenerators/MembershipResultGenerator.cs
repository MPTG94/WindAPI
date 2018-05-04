using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;

namespace WindAPI.Tests.MockResultGenerators
{
    class MembershipResultGenerator
    {
        public static MembershipResult GenerateMembershipResultForTrueResult()
        {
            MembershipResult result = new MembershipResult();
            result.isMember = true;

            return result;
        }

        public static MembershipResult GenerateMembershipResultForFalseResult()
        {
            MembershipResult result = new MembershipResult();
            result.isMember = false;

            return result;
        }
    }
}

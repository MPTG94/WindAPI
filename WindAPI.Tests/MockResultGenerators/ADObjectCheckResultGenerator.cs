using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;
using WindAPI.Tests.TestConfig;

namespace WindAPI.Tests.MockResultGenerators
{
    class ADObjectCheckResultGenerator
    {
        public static ADObjectCheckResult GenerateADObjectCheckResultForValidUser()
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.code = 0;
            result.adObject = TestConstants.UserPrimary;
            result.queryResult = new List<string>() { "user" };
            return result;
        }

        public static ADObjectCheckResult GenerateADObjectCheckResultForValidGroup()
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.code = 1;
            result.adObject = TestConstants.GroupPrime;
            result.queryResult = new List<string>() { "group" };

            return result;
        }

        public static ADObjectCheckResult GenerateADObjectCheckResultForValidComputer()
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.code = 2;
            result.adObject = TestConstants.ComputerPrimary;
            result.queryResult = new List<string>() { "computer" };

            return result;
        }

        public static ADObjectCheckResult GenerateADObjectCheckResultForValidUserAndComputer()
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.code = 3;
            result.adObject = TestConstants.UserAndComputer;
            result.queryResult = new List<string>() { "user", "computer" };

            return result;
        }

        public static ADObjectCheckResult GenerateADObjectCheckResultForValidGroupAndComputer()
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.code = 4;
            result.adObject = TestConstants.GroupAndComputer;
            result.queryResult = new List<string>() { "group", "computer" };

            return result;
        }

        public static ADObjectCheckResult GenerateADObjectCheckResultForNonExistingADObject()
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.code = 5;
            result.adObject = TestConstants.InvalidADObject;
            result.queryResult = new List<string>() { "no such AD object" };

            return result;
        }
    }
}

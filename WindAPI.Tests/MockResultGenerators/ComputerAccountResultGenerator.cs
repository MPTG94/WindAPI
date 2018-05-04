using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindAPI.Classes.ResultObjects;
using WindAPI.Handlers;
using WindAPI.Tests.TestConfig;

namespace WindAPI.Tests.MockResultGenerators
{
    class ComputerAccountResultGenerator
    {
        /// <summary>
        /// Creates a mock result for CreateComputerAccount endpoint for creation of new CA in test
        /// </summary>
        /// <param name="newOU">Is the computer acccount created in a new OU.</param>
        /// <param name="site">The site to create the computer acccount in</param>
        /// <returns>An appropriate ComputerAccountResult</returns>
        public static ComputerAccountResult GenerateResultForNewCAInTest(bool newOU, string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Computer Account created successfully.";
            result.serverName = TestConstants.NewComputerAccountTest;
            site = StringHandler.ToTitleCase(site);
            if (newOU)
            {
                result.objectADPath =
                    $"LDAP://CN={TestConstants.NewComputerAccountTest},OU={TestConstants.NewOrganizationalUnitTest},OU={site},[DEFAULT_DIRECTORY_PATH]";
            }
            else
            {
                result.objectADPath =
                    $"LDAP://CN={TestConstants.NewComputerAccountTest},OU={TestConstants.ExistingOrganizationalUnitTest},OU={site},[DEFAULT_DIRECTORY_PATH]";
            }

            return result;
        }

        public static ComputerAccountResult GenerateResultForNewCAInProd(bool newOU, string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Computer Account created successfully.";
            result.serverName = TestConstants.NewComputerAccountProd;
            site = StringHandler.ToTitleCase(site);
            if (newOU)
            {
                result.objectADPath =
                    $"LDAP://CN={TestConstants.NewComputerAccountProd},OU={TestConstants.NewOrganizationalUnitProd},OU={site},[DEFAULT_DIRECTORY_PATH]";
            }
            else
            {
                result.objectADPath =
                    $"LDAP://CN={TestConstants.NewComputerAccountProd},OU={TestConstants.ExistingOrganizationalUnitProd},OU={site},[DEFAULT_DIRECTORY_PATH]";
            }

            return result;
        }

        public static ComputerAccountResult GenerateResultForExistingCAInTest(string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Computer Account already exists.";
            result.serverName = TestConstants.ExistingComputerAccountTest;

            site = StringHandler.ToTitleCase(site);

            result.objectADPath =
                $"CN={TestConstants.ExistingComputerAccountTest},OU={TestConstants.ExistingOrganizationalUnitTest},OU={site},[DEFAULT_DIRECTORY_PATH]";

            return result;
        }

        public static ComputerAccountResult GenerateResultForExistingCAInProd(string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Computer Account already exists.";
            result.serverName = TestConstants.ExistingComputerAccountProd;

            site = StringHandler.ToTitleCase(site);

            result.objectADPath =
                $"CN={TestConstants.ExistingComputerAccountProd},OU={TestConstants.ExistingOrganizationalUnitProd},OU={site},[DEFAULT_DIRECTORY_PATH]";

            return result;
        }

        public static ComputerAccountResult GenerateResultForInvalidEnvironmentTest()
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Invalid site/environment provided.";
            result.serverName = TestConstants.NewComputerAccountTest;
            result.objectADPath = string.Empty;

            return result;
        }

        public static ComputerAccountResult GenerateResultForInvalidEnvironmentProd()
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Invalid site/environment provided.";
            result.serverName = TestConstants.NewComputerAccountProd;
            result.objectADPath = string.Empty;

            return result;
        }

        public static ComputerAccountResult GenerateResultForInvalidSiteTest()
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Invalid site/environment provided.";
            result.serverName = TestConstants.NewComputerAccountTest;
            result.objectADPath = string.Empty;

            return result;
        }

        public static ComputerAccountResult GenerateResultForInvalidSiteProd()
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Invalid site/environment provided.";
            result.serverName = TestConstants.NewComputerAccountProd;
            result.objectADPath = string.Empty;

            return result;
        }

        public static ComputerAccountResult GenerateResultForTooLongCANameTest()
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Computer Account name longer than 15 characters.";
            result.serverName = TestConstants.TooLongComputerAccountNameTest;

            return result;
        }

        public static ComputerAccountResult GenerateResultForTooLongCANameProd()
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "create";
            result.message = "Computer Account name longer than 15 characters.";
            result.serverName = TestConstants.TooLongComputerAccountNameProd;

            return result;
        }

        public static ComputerAccountResult GenerateResultForCADeleteTest(string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "delete";
            result.serverName = TestConstants.PermanentExistingComputerAccountToDeleteTest;
            result.message = "Computer Account deleted successfully.";
            result.objectADPath =
                $"CN={TestConstants.PermanentExistingComputerAccountToDeleteTest},OU={TestConstants.ExistingOrganizationalUnitTest},OU={site},[DEFAULT_DIRECTORY_PATH]";

            return result;
        }

        public static ComputerAccountResult GenerateResultForCADeleteProd(string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "delete";
            result.serverName = TestConstants.NewComputerAccountProd;
            result.message = "Computer Account deleted successfully.";
            result.objectADPath =
                $"CN={TestConstants.NewComputerAccountProd},OU={TestConstants.ExistingOrganizationalUnitProd},OU={site},[DEFAULT_DIRECTORY_PATH]";

            return result;
        }

        public static ComputerAccountResult GenerateResultForNonExistingCADeleteTest(string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "delete";
            result.serverName = TestConstants.NonExistingComputerAccountToDeleteTest;
            result.message = "No such Computer Account.";
            result.objectADPath = string.Empty;

            return result;
        }

        public static ComputerAccountResult GenerateResultForNonExistingCADeleteProd(string site)
        {
            ComputerAccountResult result = new ComputerAccountResult();
            result.action = "delete";
            result.serverName = TestConstants.NonExistingComputerAccountToDeleteProd;
            result.message = "No such Computer Account.";
            result.objectADPath = string.Empty;

            return result;
        }
    }
}

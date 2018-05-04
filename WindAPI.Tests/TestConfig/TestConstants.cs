using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindAPI.Tests.TestConfig
{
    /// <summary>
    /// Class used for static test configuration variables
    /// </summary>
    class TestConstants
    {
        #region UserTestConsts

        /// <summary>
        /// Primary user for user related tests.
        /// </summary>
        public static readonly string UserPrimary = "qauser";

        /// <summary>
        /// Secondary user for user related tests.
        /// </summary>
        public static readonly string UserSecondary = "qauser2";

        /// <summary>
        /// string array with single user for user string arrays related tests.
        /// </summary>
        public static readonly string[] UserListSingle = new string[1] { UserPrimary };

        /// <summary>
        /// string array with multiple users for user string arrays related tests.
        /// </summary>
        public static readonly string[] UserListMulti = new string[2] { UserPrimary, UserSecondary };

        /// <summary>
        /// User for No Matching Exception tests.
        /// </summary>
        public static readonly string UserNoMatching = "nomatchuser";

        /// <summary>
        /// Primary user for invalid user tests.
        /// </summary>
        public static readonly string UserInvalidPrimary = "abcdefghiklmop";

        /// <summary>
        /// Secondary user for invalid user tests.
        /// </summary>
        public static readonly string UserInvalidSecondary = "abcdefghiklmnopqrs";

        /// <summary>
        /// string array with single invliad user for user string arrays related tests.
        /// </summary>
        public static readonly string[] InvalidUserListSingle = new string[1] { UserInvalidPrimary };

        /// <summary>
        /// string array with multiple invalid users for user string arrays related tests.
        /// </summary>
        public static readonly string[] InvalidUserListMulti = new string[2] { UserInvalidPrimary, UserInvalidSecondary };

        /// <summary>
        /// User for User And Computer AD Object tests.
        /// </summary>
        public static readonly string UserAndComputer = "usercomptest";

        #endregion

        #region GroupTestConsts

        /// <summary>
        /// Primary group for group related tests
        /// </summary>
        public static readonly string GroupPrime = "qagroup";

        /// <summary>
        /// Primary group for group info related tests
        /// </summary>
        public static readonly string GroupInfoPrime = "Domain Admins";

        /// <summary>
        /// Secondary group for group info related tests
        /// </summary>
        public static readonly string GroupInfoSecond = "Users";

        /// <summary>
        /// string array with single group for group string arrays related tests
        /// </summary>
        public static readonly string[] GroupListSingle = new string[1] { GroupInfoPrime };

        /// <summary>
        /// string array with multiple groups for group string arrays related tests
        /// </summary>
        public static readonly string[] GroupListMulti = new string[2] { GroupInfoPrime, GroupInfoSecond };

        /// <summary>
        /// Primary group for invalid group tests
        /// </summary>
        public static readonly string GroupNoMembership = "Domain Admins";

        /// <summary>
        /// Group for Group And Computer AD Object tests
        /// </summary>
        public static readonly string GroupAndComputer = "groupcomptest";

        /// <summary>
        /// Primary group for invalid group tests
        /// </summary>
        public static readonly string GroupInvalidPrime = "abcdefghiklmop";

        /// <summary>
        /// Secondary group for invalid group tests
        /// </summary>
        public static readonly string GroupInvalidSecond = "abcdefghiklmnopqrs";

        /// <summary>
        /// string array with single invalid group for group string arrays related tests
        /// </summary>
        public static readonly string[] InvalidGroupListSingle = new string[1] { GroupInvalidPrime };

        /// <summary>
        /// string array with multiple invalid groups for group string arrays related tests
        /// </summary>
        public static readonly string[] InvalidGroupListMulti = new string[2] { GroupInvalidPrime, GroupInvalidSecond }; 
    
        #endregion

        #region ServerTestConsts

        /// <summary>
        /// Primary computer for computer related tests.
        /// </summary>
        public static readonly string ComputerPrimary = "exampleserver";

        /// <summary>
        /// Primary computer for invalid computer tests.
        /// </summary>
        public static readonly string ComputerInvalidPrimary = "abcdefghiklmop";

        #endregion

        #region InvalidObjectsConsts

        /// <summary>
        /// Primary string for invalid AD object related tests.
        /// </summary>
        public static readonly string InvalidADObject = "abcdefghiklmop";

        #endregion

        #region ComputerAccountTestConstans

        /// <summary>
        /// String for a new computer account for test environment.
        /// </summary>
        public static readonly string NewComputerAccountTest = "windcadummytest";

        /// <summary>
        /// String for a new computer account for production environment.
        /// </summary>
        public static readonly string NewComputerAccountProd = "windcadummyprod";

        /// <summary>
        /// String for an existing computer account for test environment.
        /// </summary>
        public static readonly string ExistingComputerAccountTest = "windtestexca";

        /// <summary>
        /// String for an existing computer account for production environment.
        /// </summary>
        public static readonly string ExistingComputerAccountProd = "windprodexca";

        /// <summary>
        /// String for a computer account which is used in deletion tests for test environment
        /// </summary>
        public static readonly string PermanentExistingComputerAccountToDeleteTest = "testcafordeltests";

        /// <summary>
        /// String for a computer account which is used in deletion tests for production environment
        /// </summary>
        public static readonly string PermanentExistingComputerAccountToDeleteProd = "prodcafordeltests";

        /// <summary>
        /// String for a computer account which doesn't exist for test environment.
        /// </summary>
        public static readonly string NonExistingComputerAccountToDeleteTest = "nocatest";

        /// <summary>
        /// String for a computer account which doesn't exist for production environment.
        /// </summary>
        public static readonly string NonExistingComputerAccountToDeleteProd = "nocaprod";

        /// <summary>
        /// String for a computer account name that is longer than 15 characters
        /// for test environment.
        /// </summary>
        public static readonly string TooLongComputerAccountNameTest = "longcomputeraccountnametest";

        /// <summary>
        /// String for a computer account name that is longer than 15 characters
        /// for production environment.
        /// </summary>
        public static readonly string TooLongComputerAccountNameProd = "longcomputeraccountnameprod";

        /// <summary>
        /// String for a new organizational unit for test environment.
        /// </summary>
        public static readonly string NewOrganizationalUnitTest = "windoudummytest";

        /// <summary>
        /// String for a new organizational unit for production environment.
        /// </summary>
        public static readonly string NewOrganizationalUnitProd = "windoudummytest";

        /// <summary>
        /// String for an existing organization unit for test environment.
        /// </summary>
        public static readonly string ExistingOrganizationalUnitTest = "windpermanentoutest";

        /// <summary>
        /// String for an existing organization unit for test production.
        /// </summary>
        public static readonly string ExistingOrganizationalUnitProd = "windpermanentouprod";

        /// <summary>
        /// String for SITE1 site.
        /// </summary>
        public static readonly string SitePrimary = "SITE1";

        /// <summary>
        /// String for SITE2 site.
        /// </summary>
        public static readonly string SiteSecondary = "SITE2";

        /// <summary>
        /// String for an invalid site.
        /// </summary>
        public static readonly string InvalidSite = "Invalid";

        /// <summary>
        /// String for Test environment.
        /// </summary>
        public static readonly string TestEnvironment = "test";

        /// <summary>
        /// String for Production environment.
        /// </summary>
        public static readonly string ProductionEnvironment = "production";

        /// <summary>
        /// String for an invalid environment.
        /// </summary>
        public static readonly string InvalidEnvironment = "Invalid";

        #endregion

        #region CheckpointTokenConstants

        /// <summary>
        /// Primary checkpoint access token to perform actions with administrator permissions.
        /// </summary>
        public static readonly string checkpointAdminAcccessToken = "[TOKEN]";

        /// <summary>
        /// Primary checkpoint access token to perform actions without administrator permissions.
        /// </summary>
        public static readonly string checkpointNonAdminAcccessToken = "[TOKEN]";

        #endregion

        #region RunCommandTestConstants

        /// <summary>
        /// Primary valid server to run command on.
        /// </summary>
        public static readonly string validServerName = "exampleserver";

        /// <summary>
        /// Primary invalid server to run command on.
        /// </summary>
        public static readonly string invalidServerName = "mmabcdefg";

        /// <summary>
        /// Primary valid command to execute remotely.
        /// </summary>
        public static readonly string validCommand = "hostname";

        /// <summary>
        /// Primary invalid command to execute remotely.
        /// </summary>
        public static readonly string invalidCommand = "abcdefgh";

        #endregion

        #region AddAdminTestConstants

        /// <summary>
        /// Hostname of Windows 2003 Server.
        /// </summary>
        /// 
        public static readonly string server2003Hostname = "server2003";
        /// <summary>
        /// Hostname of Windows 2008 Server.
        /// </summary>
        public static readonly string server2008Hostname = "server2008";

        /// <summary>
        /// Hostname of Windows 2008R2 Server.
        /// </summary>
        public static readonly string server2008R2Hostname = "server2008R2";

        /// <summary>
        /// Hostname of Windows 2012R2 Server.
        /// </summary>
        public static readonly string server2012R2Hostname = "server2012";

        /// <summary>
        /// Hostname of Windows 2016 Server.
        /// </summary>
        public static readonly string server2016Hostname = "server2016";

        #endregion
    }
}

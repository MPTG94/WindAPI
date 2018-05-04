using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WindAPI.Classes.RequestObjects;
using WindAPI.Classes.ResultObjects;
using WindAPI.Config;

namespace WindAPI.Handlers
{
    public static class ActiveDirectory
    {
        /// <summary>
        /// A string containing the AD domain name
        /// </summary>
        public static readonly string Domain = Constants.domain;
        /// <summary>
        /// This method receives a username and a group and checks whether
        /// the user is part of the AD group
        /// </summary>
        /// <param name="username">The user to check membership for</param>
        /// <param name="group">The AD group to check if the user is a member
        /// of</param>
        /// <returns>True if the user is a member of the input group
        /// group. False otherwise</returns>
        public static bool IsUserMemberOf(string username, string group)
        {
            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Find the user in AD.
            UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, username);

            // Find the input group in AD.
            GroupPrincipal inputGroup = GroupPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, group);

            if ((user != null) && (inputGroup != null))
            {
                // Check if the user is a member of the input group.
                if (inputGroup.GetMembers(true).Contains(user))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method receives the name of an AD object and checks whether
        /// the object is a user, group or something else and returns the result.
        /// </summary>
        /// <param name="adObject">The name of the AD Object to check it's type.</param>
        /// <returns>an ADObjectCheckResult object containing the adObject, a list result if
        /// the object is a user, group or computer.
        /// The method returns an integer code to differentiate results:
        /// 0 - The AD Object is a user
        /// 1 - The AD Object is a group
        /// 2 - The AD Object is a computer
        /// 3 - The AD Object is a user and a computer
        /// 4 - The AD Object is a group and a computer
        /// 5 - There is no such AD Object</returns>
        public static ADObjectCheckResult CheckADObjectType(string adObject)
        {
            ADObjectCheckResult result = new ADObjectCheckResult();
            result.adObject = adObject;

            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain, Constants.adQueryUser, Constants.adQueryPassword);

            // Find the object in AD.
            // We check for each AD Object seperately to avoid MultipleMatchesException.
            UserPrincipal userObject = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, adObject);
            GroupPrincipal groupObject = GroupPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, adObject);
            ComputerPrincipal computerObject = ComputerPrincipal.FindByIdentity(pc, adObject);

            if (userObject != null)
            {
                if (computerObject == null)
                {
                    // The input AD Object is a user.
                    result.code = 0;
                    result.queryResult = new List<string>() { "user" };
                }
                else
                {
                    // The input AD Object is a user and a computer.
                    result.code = 3;
                    result.queryResult = new List<string>() { "user", "computer" };
                }
            }
            else if (groupObject != null)
            {
                if (computerObject == null)
                {
                    // The input AD Object is a group.
                    result.code = 1;
                    result.queryResult = new List<string>() { "group" };
                }
                else
                {
                    // The input AD Object is a group and a computer.
                    result.code = 4;
                    result.queryResult = new List<string>() { "group", "computer" };
                }
            }
            else if (computerObject != null)
            {
                // The input AD Object is a computer.
                result.code = 2;
                result.queryResult = new List<string>() { "computer" };
            }
            else
            {
                // No such object in AD
                result.code = 5;
                result.queryResult = new List<string>() { "no such AD object" };
            }

            return result;
        }

        /// <summary>
        /// This method receives a UserMembershipResult object containing a username to check
        /// and get all of the AD groups the user is a member of
        /// </summary>
        /// <param name="membershipResult">A UserMembershipResult object containing a username
        /// to check.</param>
        /// <returns>A UserMembershipResult object containing the username to check, a result message,
        /// a status code for the AD query and the list of groups the user is a member of.</returns>
        public static UserMembershipResult GetAllGroupsByUser(UserMembershipResult membershipResult)
        {
            // Create output group names list.
            membershipResult.groups = new List<string>();
            membershipResult.message = string.Empty;

            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain, Constants.adQueryUser, Constants.adQueryPassword);

            // Find the user in AD.
            UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, membershipResult.user);

            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                // Defining iterator object.
                var iterator = groups.GetEnumerator();
                using (iterator)
                {
                    while (iterator.MoveNext())
                    {
                        try
                        {
                            Principal p = iterator.Current;
                            if (p is GroupPrincipal)
                            {
                                membershipResult.groups.Add(p.SamAccountName);
                            }
                        }
                        catch (NoMatchingPrincipalException pex)
                        {
                            if (membershipResult.code != 1)
                            {
                                membershipResult.message = pex.Message;
                                membershipResult.code = 1;
                            }
                            continue;
                        }
                    }
                }

                if (membershipResult.message == string.Empty)
                {
                    membershipResult.message = "Group list generated successfully";
                    membershipResult.code = 0;
                }

                return membershipResult;
            }
            membershipResult.message = "Input user not found in Active Directory";
            membershipResult.code = 2;
            return membershipResult;
        }

        /// <summary>
        /// This method receives a UserInfoResult object containing a username to check
        /// and get fields from the user's AD object
        /// </summary>
        /// <param name="infoResult">A UserInfoResult object containing the username</param>
        /// <returns>A UserInfoResult containing the username and additional AD fields</returns>
        public static UserInfoResult GetUserInfoByUsername(UserInfoResult infoResult)
        {
            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Find the user in AD.
            UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, infoResult.sAMAccountName);

            if (user != null)
            {
                infoResult.code = 0;
                // Get directory entry for user to extract extra properties.
                DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;
                // Checking extra properties to make sure they exist and add them to result.
                if (de.Properties.Contains("cn"))
                {
                    infoResult.cn = de.Properties["cn"].Value.ToString();
                }
                if (de.Properties.Contains("givenName"))
                {
                    infoResult.firstName = de.Properties["givenName"].Value.ToString();
                }
                if (de.Properties.Contains("sn"))
                {
                    infoResult.lastName = de.Properties["sn"].Value.ToString();
                }
                if (de.Properties.Contains("title"))
                {
                    infoResult.title = de.Properties["title"].Value.ToString();
                }
                if (de.Properties.Contains("telephoneNumber"))
                {
                    infoResult.telephoneNumber = de.Properties["telephoneNumber"].Value.ToString();
                }
                if (de.Properties.Contains("displayName"))
                {
                    infoResult.displayName = de.Properties["displayName"].Value.ToString();
                }
                if (de.Properties.Contains("extensionAttribute11"))
                {
                    infoResult.organizationHierarchy = de.Properties["extensionAttribute11"].Value.ToString();
                }
                if (de.Properties.Contains("mail"))
                {
                    infoResult.mail = de.Properties["mail"].Value.ToString();
                }
                if (de.Properties.Contains("pager"))
                {
                    infoResult.voip = de.Properties["pager"].Value.ToString();
                }
            }
            else
            {
                // User not found in AD.
                infoResult.code = 1;
            }

            return infoResult;
        }

        /// <summary>
        /// This method receives a GroupInfoResult object containing a groupname to check
        /// and get fields from the group's AD object
        /// </summary>
        /// <param name="infoResult">A GroupInfoResult object containing the groupname</param>
        /// <returns>A GroupInfoResult containing the groupname and additional AD fields</returns>
        public static GroupInfoResult GetGroupInfoByGroupname(GroupInfoResult infoResult)
        {
            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Find the group in AD.
            GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, infoResult.sAMAccountName);

            if (group != null)
            {
                infoResult.code = 0;
                // Get directory entry for group to extract extra properties.
                DirectoryEntry de = group.GetUnderlyingObject() as DirectoryEntry;
                // Checking extra properties to make sure they exist and add them to result.
                if (de.Properties.Contains("cn"))
                {
                    infoResult.cn = de.Properties["cn"].Value.ToString();
                }
                if (de.Properties.Contains("displayName"))
                {
                    infoResult.displayName = de.Properties["displayName"].Value.ToString();
                }
                if (de.Properties.Contains("description"))
                {
                    infoResult.description = de.Properties["description"].Value.ToString();
                }
                if (de.Properties.Contains("mail"))
                {
                    infoResult.mail = de.Properties["mail"].Value.ToString();
                }
            }
            else
            {
                // Group not found in AD.
                infoResult.code = 1;
            }

            return infoResult;
        }

        /// <summary>
        /// This method receives a ComputerAccountRequest object containing the server name,
        /// The site name and the project name and creates the computer account, a new OU
        /// will be created if necessary.
        /// </summary>
        /// <param name="request">A ComputerAccountRequest object containing the server name,
        /// the site name and the project name.</param>
        /// <returns>A ComputerAccountResult containing the action performed, a success/failure message,
        /// the name of the server which the computer account belongs to and the path of the object in AD.</returns>
        public static ComputerAccountResult CreateComputerAccount(ComputerAccountRequest request)
        {
            // Set up the result object.
            ComputerAccountResult result = new ComputerAccountResult()
            {
                action = "create",
                message = string.Empty,
                serverName = request.serverName,
                objectADPath = string.Empty
            };

            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Check if an existing computer account exists in AD.
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(pc, request.serverName);

            // Creating DirectoryEntry object.
            DirectoryEntry adSiteRoot;
            DirectoryEntry newOU;
            DirectoryEntry newCA;

            if (computer == null)
            {
                // No such computer account, creating.

                // Initializing DirectoryEntry object.
                adSiteRoot = GetDirectoryEntryBySite(request);

                if (adSiteRoot == null)
                {
                    result.message = "Invalid site/environment provided.";
                    result.objectADPath = string.Empty;
                }
                else
                {
                    // Generating path of the target OU by project name.
                    newOU = adSiteRoot.Children.Add($"OU={request.projectName}", "OrganizationalUnit");
                    // Checking if the OU already exists.
                    if (!DirectoryEntry.Exists(newOU.Path))
                    {
                        // OU doesn't exist, Creating new OU for the Computer Account.
                        newOU.CommitChanges();
                    }

                    // Creating new Computer Account in the OU.
                    newCA = newOU.Children.Add($"CN={request.serverName}", "computer");

                    // Applying Server Name in uppercase as the sAMAccountName because by default AD generates a random
                    // GUID for new servers.
                    // Adding a trailing $ due to pre-windows 2000 server name requirements.
                    newCA.Properties["sAMAccountName"].Value = request.serverName.ToUpper() + "$";

                    // Defining the properties PASSWD_NOTREQD and WORKSTATION_TRUST_ACCOUNT.
                    newCA.Properties["userAccountControl"].Value = 0x1020;

                    newCA.CommitChanges();

                    result.message = "Computer Account created successfully.";
                    result.objectADPath = newCA.Path;
                }
            }
            else
            {
                // Computer already exists in AD.
                result.message = "Computer Account already exists.";
                result.objectADPath = computer.DistinguishedName;
            }

            return result;
        }

        /// <summary>
        /// This method receives a string containing the server name, and deletes it's computer account.
        /// </summary>
        /// <param name="serverName">a string containing the server name.</param>
        /// <returns>A ComputerAccountResult containing the action performed, a success/failure message,
        /// the name of the server which the computer account belongs to and the path of the object in AD.</returns>
        public static ComputerAccountResult DeleteComputerAccount(string serverName)
        {
            // Set up the result object.
            ComputerAccountResult result = new ComputerAccountResult()
            {
                action = "delete",
                message = string.Empty,
                serverName = serverName,
                objectADPath = string.Empty
            };

            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Check if an existing computer account exists in AD.
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(pc, serverName);

            if (computer != null)
            {
                result.objectADPath = computer.DistinguishedName;
                computer.Delete();
                result.message = "Computer Account deleted successfully.";
            }
            else
            {
                result.message = "No such Computer Account.";
            }

            return result;
        }

        /// <summary>
        /// This method receives a ComputerAccountRequest object containing the server name,
        /// The site name and the project name and creates the computer account, a new OU
        /// will be created if necessary.
        /// </summary>
        /// <param name="request">A ComputerAccountRequest object containing the server name,
        /// the site name and the project name.</param>
        public static void CreateComputerAccountVoid(ComputerAccountRequest request)
        {
            // Set up the result object.
            ComputerAccountResult result = new ComputerAccountResult()
            {
                action = "create",
                message = string.Empty,
                serverName = request.serverName,
                objectADPath = string.Empty
            };

            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Check if an existing computer account exists in AD.
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(pc, request.serverName);

            // Creating DirectoryEntry object.
            DirectoryEntry adSiteRoot;
            DirectoryEntry newOU;
            DirectoryEntry newCA;

            while (computer != null)
            {
                computer = ComputerPrincipal.FindByIdentity(pc, request.serverName);
            }

            if (computer == null)
            {
                // No such computer account, creating.

                // Initializing DirectoryEntry object.
                adSiteRoot = GetDirectoryEntryBySite(request);

                if (adSiteRoot == null)
                {
                    result.message = "Invalid site/environment provided.";
                    result.objectADPath = string.Empty;
                }
                else
                {
                    // Generating path of the target OU by project name.
                    newOU = adSiteRoot.Children.Add($"OU={request.projectName}", "OrganizationalUnit");
                    // Checking if the OU already exists.
                    if (!DirectoryEntry.Exists(newOU.Path))
                    {
                        // OU doesn't exist, Creating new OU for the Computer Account.
                        newOU.CommitChanges();
                    }

                    // Creating new Computer Account in the OU.
                    newCA = newOU.Children.Add($"CN={request.serverName}", "computer");

                    // Applying Server Name in uppercase as the sAMAccountName because by default AD generates a random
                    // GUID for new servers.
                    // Adding a trailing $ due to pre-windows 2000 server name requirements.
                    newCA.Properties["sAMAccountName"].Value = request.serverName.ToUpper() + "$";

                    // Defining the properties PASSWD_NOTREQD and WORKSTATION_TRUST_ACCOUNT.
                    newCA.Properties["userAccountControl"].Value = 0x1020;

                    newCA.CommitChanges();

                    result.message = "Computer Account created successfully.";
                    result.objectADPath = newCA.Path;
                }
            }
            else
            {
                // Computer already exists in AD.
                result.message = "Computer Account already exists.";
                result.objectADPath = computer.DistinguishedName;
            }
        }

        public static void DeleteComputerAccountVoid(string serverName)
        {
            // Set up domain context.
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain);

            // Check if an existing computer account exists in AD.
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(pc, serverName);

            while (computer == null)
            {
                computer = ComputerPrincipal.FindByIdentity(pc, serverName);
            }

            if (computer != null)
            {
                computer.Delete();
            }
        }

        public static void DeleteOrganizationalUnitTreeVoid(string environment, string ouName)
        {
            DirectoryEntry adSiteRoot;

            // Creating DirectoryEntry object.
            if (environment.ToLower() == "production")
            {
                adSiteRoot = new DirectoryEntry("LDAP://[DIRECTORY_PATH]");
            }
            else
            {
                adSiteRoot = new DirectoryEntry("LDAP://[DIRECTORY_PATH]");
            }

            DirectoryEntry ouToDelete;

            string ouPath = $"OU={ouName}";
            ouToDelete = adSiteRoot.Children.Find(ouPath, "OrganizationalUnit");
            ouToDelete.DeleteTree();
            ouToDelete.CommitChanges();
        }

        private static DirectoryEntry GetDirectoryEntryBySite(ComputerAccountRequest request)
        {
            // Creating DirectoryEntry.
            DirectoryEntry adSiteRoot;

            // Determining selected environment for computer accoount.
            if (request.environment.ToLower() == "production")
            {
                // Determining selected site for computer accoount.
                switch (request.siteName.ToLower())
                {
                    case "SITE1":
                        adSiteRoot = new DirectoryEntry($"LDAP://[DIRECTORY_PATH]");
                        break;
                    case "SITE2":
                        adSiteRoot = new DirectoryEntry($"[DIRECTORY_PATH]");
                        break;
                    case "SITE3":
                        adSiteRoot = new DirectoryEntry($"[DIRECTORY_PATH]");
                        break;
                    case "SITE4":
                        adSiteRoot = new DirectoryEntry($"[DIRECTORY_PATH]");
                        break;
                    case "SITE5":
                        adSiteRoot = new DirectoryEntry($"[DIRECTORY_PATH]");
                        break;
                    default:
                        adSiteRoot = null;
                        break;
                }
            }
            else if (request.environment.ToLower() == "test")
            {
                // Determining selected site for computer accoount.
                switch (request.siteName.ToLower())
                {
                    case "SITE1":
                        adSiteRoot = new DirectoryEntry($"LDAP://[DIRECTORY_PATH]");
                        break;
                    case "SITE2":
                        adSiteRoot = new DirectoryEntry($"LDAP://[DIRECTORY_PATH]");
                        break;
                    case "SITE3":
                        adSiteRoot = new DirectoryEntry($"LDAP://[DIRECTORY_PATH]");
                        break;
                    case "SITE4":
                        adSiteRoot = new DirectoryEntry($"LDAP://[DIRECTORY_PATH]");
                        break;
                    case "SITE5":
                        adSiteRoot = new DirectoryEntry($"LDAP://[DIRECTORY_PATH]");
                        break;
                    default:
                        adSiteRoot = null;
                        break;
                }

            }
            else
            {
                adSiteRoot = null;
            }

            return adSiteRoot;
        }
    }
}
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Web;
using WindAPI.Classes.RequestObjects;
using WindAPI.Classes.ResultObjects;
using WindAPI.Config;

namespace WindAPI.Handlers
{
    /// <summary>
    /// Class to handle server Actions.
    /// </summary>
    public class ServerHandler
    {
        /// <summary>
        /// This method receieves a server name and AdministratorRequest object and adds the
        /// users and groups from the request as admins to the specified server.
        /// </summary>
        /// <param name="serverName">The name of the server to add administators to.</param>
        /// <param name="request">A AddAdministratorRequest object that contains the users and groups that need to be
        /// added as administrators.</param>
        /// <returns>A AddAdministratorResult object containing the lists of users and groups that were added/were already admins/
        /// weren't added and a result message.</returns>
        public static AddAdministratorResult AddAdministratorsByRequestObject(string serverName, AddAdministratorRequest request)
        {
            // Creating result object to return.
            AddAdministratorResult result = new AddAdministratorResult(serverName);

            // Checking that we receieved users that need to be added.
            if (request.users != null)
            {
                // Iterating all users and adding as administrators.
                foreach (string user in request.users)
                {
                    switch (AddAdministrator(serverName, user))
                    {
                        case 0:
                            // Group added successfully.
                            result.addedUsers.Add(user);
                            break;
                        case 1:
                            // Group already administrator.
                            result.alreadyAdminUsers.Add(user);
                            break;
                        case 2:
                            // User doesn't exist in Active Directory.
                            result.failedUsers.Add(user);
                            result.message = "Some of the specified users or groups don't exist in Active Directory.";
                            break;
                        case 3:
                            // Some administrators on the server were already deleted from AD, Wind can't query the administrators.
                            result.failedUsers.Add(user);
                            result.message = "Some administrators on the server were already deleted from AD, Wind can't query the administrators.";
                            break;
                        case 4:
                            // The target server took to long to respond and connection the server failed.
                            result.failedUsers.Add(user);
                            result.message = "The target server took to long to respond and connection the server failed.";
                            break;
                        case 5:
                            // An unidentified error occured.
                            result.failedUsers.Add(user);
                            result.message = "A general error occured.";
                            break;
                        default:
                            // An unidentified error occured.
                            result.message = "A general error occured.";
                            break;
                    }
                }
            }

            // Checking that we receieved groups that need to be added.
            if (request.groups != null)
            {
                // Iterating all groups and adding as administrators.
                foreach (string group in request.groups)
                {
                    switch (AddAdministrator(serverName, group))
                    {
                        case 0:
                            // Group added successfully.
                            result.addedGroups.Add(group);
                            break;
                        case 1:
                            // Group already administrator.
                            result.alreadyAdminGroups.Add(group);
                            break;
                        case 2:
                            // Group doesn't exist in Active Directory.
                            result.failedGroups.Add(group);
                            result.message = "Some of the specified users or groups don't exist in Active Directory.";
                            break;
                        case 3:
                            // Some administrators on the server were already deleted from AD, Wind can't query the administrators.
                            result.failedGroups.Add(group);
                            result.message = "Some administrators on the server were already deleted from AD, Wind can't query the administrators.";
                            break;
                        case 4:
                            // The target server took to long to respond and connection the server failed.
                            result.failedGroups.Add(group);
                            result.message = "The target server took to long to respond and connection the server failed.";
                            break;
                        case 5:
                            // An unidentified error occured.
                            result.failedGroups.Add(group);
                            result.message = "A general error occured.";
                            break;
                        default:
                            // An unidentified error occured.
                            result.message = "A general error occured.";
                            break;
                    }
                }
            }

            if (result.message == string.Empty)
            {
                // No error has occured, so return a message that everything worked fine.
                result.message = "All users and groups were added successfully.";
            }

            return result;
        }

        /// <summary>
        /// This method receives a server name and a user/group name and adds
        /// the input name as an admin user to the server admins.
        /// </summary>
        /// <param name="serverName">The name of the server to add administrators to.</param>
        /// <param name="newAdmin">The user/group to add as administrator.</param>
        /// <returns>A status code to differntiatie results:
        /// 0 - User/Group added successfully.
        /// 1 - User/Group is already a member of the administrators group.
        /// 2 - The AD Object is a computer
        /// 3 - A general error occured.</returns>
        public static int AddAdministrator(string serverName, string newAdmin)
        {
            try
            {
                // Set up domain context.
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, Constants.domain);

                // Set up remote server context.
                using (PrincipalContext serverContext = new PrincipalContext(ContextType.Machine, serverName, null,
                    ContextOptions.Negotiate, $"{Constants.domain}\\{Constants.adQueryUser}", Constants.adQueryPassword))
                {
                    // Getting administrators group on the remote server.
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(serverContext, "Administrators");

                    // Checking that we got the local admins group successfully.
                    if (group != null)
                    {
                        group.Members.Add(domainContext, IdentityType.SamAccountName, newAdmin);
                        group.Save();
                    }
                }

                return 0;
            }
            catch (PrincipalOperationException ex) when (ex.Message == "The specified account name is already a member of the group.\r\n")
            {
                // The user/group we tried to add is already a member of the admins groups.
                return 1;
            }
            catch (PrincipalExistsException ex) when (ex.Message ==  "The principal already exists in the store.")
            {
                // The user/group we tried to add is already a member of the admins groups.
                return 1;
            }
            catch (NoMatchingPrincipalException ex) when (ex.Message == "No principal matching the specified parameters was found.")
            {
                // The user/group we tried to add does not exist in Active Directory.
                return 2;
            }
            catch (PrincipalOperationException ex) when (ex.Message == "An error (1332) occurred while enumerating the group membership.  The member's SID could not be resolved.")
            {
                // The server has some administrators which have already been deleted from AD but not from the server list.
                return 3;
            }
            catch (FileNotFoundException ex) when (ex.Message == "The network path was not found.\r\n")
            {
                // The target server took to long to respond and connection the server failed.
                return 4;
            }
            catch (Exception ex)
            {
                string exType = ex.GetType().ToString();
                System.Diagnostics.Debug.WriteLine(exType);
                return 5;
            }

        }

        /// <summary>
        /// This method receives a server name and a user/group name and removes
        /// the input name from the server admins.
        /// </summary>
        /// <param name="serverName">The name of the server to remove the administrator from.</param>
        /// <param name="oldAdmin">The user/group to remove.</param>
        public static void RemoveAdministratorVoid(string serverName, string oldAdmin)
        {
            // Set up domain context.
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, Constants.domain);

            // Set up remote server context.
            using (PrincipalContext serverContext = new PrincipalContext(ContextType.Machine, serverName, null,
                ContextOptions.Negotiate, $"{Constants.domain}\\{Constants.adQueryUser}", Constants.adQueryPassword))
            {
                // Getting administrators group on the remote server.
                GroupPrincipal group = GroupPrincipal.FindByIdentity(serverContext, "Administrators");

                // Checking that we got the local admins group successfully.
                if (group != null)
                {
                    group.Members.Remove(domainContext, IdentityType.SamAccountName, oldAdmin);
                    group.Save();
                }
            }
        }
    }
}
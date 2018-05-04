// Uncomment the following to provide samples for PageResult<T>. Must also add the Microsoft.AspNet.WebApi.OData
// package to your project.
////#define Handle_PageResultOfT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using WindAPI.Classes.RequestObjects;
using WindAPI.Classes.ResultObjects;
#if Handle_PageResultOfT
using System.Web.Http.OData;
#endif

namespace WindAPI.Areas.HelpPage
{
    /// <summary>
    /// Use this class to customize the Help Page.
    /// For example you can set a custom <see cref="System.Web.Http.Description.IDocumentationProvider"/> to supply the documentation
    /// or you can provide the samples for the requests/responses.
    /// </summary>
    public static class HelpPageConfig
    {
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "WindAPI.Areas.HelpPage.TextSample.#ctor(System.String)",
            Justification = "End users may choose to merge this string with existing localized resources.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
            MessageId = "bsonspec",
            Justification = "Part of a URI.")]
        public static void Register(HttpConfiguration config)
        {
            //// Uncomment the following to use the documentation from XML documentation file.
            //config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            // Setting API Documenation file path.
            config.SetDocumentationProvider(new XmlDocumentationProvider(
                HttpContext.Current.Server.MapPath("~/App_Data/ApiXmlDocumentation.xml")));

            //// Uncomment the following to use "sample string" as the sample for all actions that have string as the body parameter or return type.
            //// Also, the string arrays will be used for IEnumerable<string>. The sample objects will be serialized into different media type 
            //// formats by the available formatters.
            //config.SetSampleObjects(new Dictionary<Type, object>
            //{
            //    {typeof(string), "sample string"},
            //    {typeof(IEnumerable<string>), new string[]{"sample 1", "sample 2"}}
            //});

            config.SetSampleObjects(new Dictionary<Type, object>
            {
                {typeof(CommandRequest),
                    new CommandRequest
                    {
                        commandToExecute = "hostname",
                    }
                },

                {typeof(ExecutionResult),
                    new ExecutionResult
                    {
                        commandExitCode = 0,
                        resultMessage = "exampleserver",
                        resultError = null
                    }
                },

                {typeof(UserMembershipResult),
                    new UserMembershipResult
                    {
                        code = 0,
                        message = "Group list generated sueccessfully",
                        user = "mptg94",
                        groups = new List<string>() { "mptggroup", "mptggroup2", "..." }
                    }
                },

                { typeof(ADObjectCheckResult),
                    new ADObjectCheckResult
                    {
                        code = 0,
                        adObject = "mptg95",
                        queryResult = new List<string>() { "user" }
                    }
                },

                { typeof(UserInfoResult),
                    new UserInfoResult
                    {
                        code = 0,
                        sAMAccountName = "mptg94",
                        cn = "Mor Paz",
                        displayName = "Mor Paz",
                        firstName = "Mor",
                        lastName = "Paz",
                        title = "Administrator",
                        mail = "1912.mor@gmail.com",
                        telephoneNumber = "[PHONE]",
                        voip = "voip - 1111",
                        organizationHierarchy = "[DIRECTORY_PATH]"
                    }
                },

                { typeof(GroupInfoResult),
                    new GroupInfoResult
                    {
                        code = 0,
                        sAMAccountName = "mptg94",
                        cn = "Mor Paz",
                        displayName = "Mor Paz",
                        description = "Administrator",
                        mail = "1912.mor@gmail.com"
                    }
                },

                { typeof(ComputerAccountRequest),
                    new ComputerAccountRequest
                    {
                        serverName = "myServer",
                        environment = "test",
                        siteName = "SITE1",
                        projectName = "mytest"
                    }
                },

                { typeof(ComputerAccountResult),
                    new ComputerAccountResult
                    {
                        action = "create",
                        message = "Computer Account created successfully.",
                        serverName = "exampleserver",
                        objectADPath = "LDAP://[DIRECTORY_PATH]"
                    }
                },

                { typeof(AddAdministratorRequest),
                   new AddAdministratorRequest
                   {
                       users = new List<string>() { "user1", "user2" },
                       groups = new List<string>() { "group1", "group2" }
                   }
                },

                { typeof(AddAdministratorResult),
                    new AddAdministratorResult("server.example.com")
                    {
                        serverName = "server.example.com",
                        addedUsers = new List<string>() { "user1", "user2" },
                        addedGroups = new List<string>() { "group1", "group2" },
                        alreadyAdminUsers = new List<string>() { "user3", "user4" },
                        alreadyAdminGroups = new List<string>() { "group3", "group4" },
                        failedUsers = new List<string>() { "user5", "user6" },
                        failedGroups = new List<string>() { "group5", "group6" },
                        message = "All users and groups were added successfully."
                    }
                }
            });

            /*ComputerAccountResult createResult = new ComputerAccountResult()
            {
                action = "create",
                message = "Computer Account created successfully.",
                serverName = "mptg94",
                objectADPath = "LDAP://[DIRECTORY_PATH]"
            };

            config.SetSampleResponse(createResult, new MediaTypeHeaderValue("application/json"), "Users", "CreateComputerAccount");

            ComputerAccountResult deleteResult = new ComputerAccountResult()
            {
                action = "delete",
                message = "Computer Account deleted successfully.",
                serverName = "mptg94",
                objectADPath = "LDAP://[DIRECTORY_PATH]"
            };

            config.SetSampleResponse(deleteResult, new MediaTypeHeaderValue("application/json"), "Users", "DeleteComputerAccount");*/

            // Extend the following to provide factories for types not handled automatically (those lacking parameterless
            // constructors) or for which you prefer to use non-default property values. Line below provides a fallback
            // since automatic handling will fail and GeneratePageResult handles only a single type.
#if Handle_PageResultOfT
            config.GetHelpPageSampleGenerator().SampleObjectFactories.Add(GeneratePageResult);
#endif

            // Extend the following to use a preset object directly as the sample for all actions that support a media
            // type, regardless of the body parameter or return type. The lines below avoid display of binary content.
            // The BsonMediaTypeFormatter (if available) is not used to serialize the TextSample object.
            config.SetSampleForMediaType(
                new TextSample("Binary JSON content. See http://bsonspec.org for details."),
                new MediaTypeHeaderValue("application/bson"));

            //// Uncomment the following to use "[0]=foo&[1]=bar" directly as the sample for all actions that support form URL encoded format
            //// and have IEnumerable<string> as the body parameter or return type.
            //config.SetSampleForType("[0]=foo&[1]=bar", new MediaTypeHeaderValue("application/x-www-form-urlencoded"), typeof(IEnumerable<string>));

            //// Uncomment the following to use "1234" directly as the request sample for media type "text/plain" on the controller named "Values"
            //// and action named "Put".
            //config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");

            //// Uncomment the following to use the image on "../images/aspNetHome.png" directly as the response sample for media type "image/png"
            //// on the controller named "Values" and action named "Get" with parameter "id".
            //config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");

            //// Uncomment the following to correct the sample request when the action expects an HttpRequestMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Get" were having string as the body parameter.
            //config.SetActualRequestType(typeof(string), "Values", "Get");

            //// Uncomment the following to correct the sample response when the action returns an HttpResponseMessage with ObjectContent<string>.
            //// The sample will be generated as if the controller named "Values" and action named "Post" were returning a string.
            //config.SetActualResponseType(typeof(string), "Values", "Post");

            // Configuring actual return types for Web API Endpoints.

            #region Actions Response Types

            // RunCommand Response Type Config.
            config.SetActualResponseType(typeof(ExecutionResult), "Actions", "RunCommand");
            
            // AddAdmin Response Type Config.
            config.SetActualResponseType(typeof(AddAdministratorResult), "Actions", "AddAdmin");

            #endregion

            #region Computers Response Types

            // CreateComputerAccount Response Type Config.
            config.SetActualResponseType(typeof(ComputerAccountResult), "Computers", "Post");

            // DeleteComputerAccount Response Type Config.
            config.SetActualResponseType(typeof(ComputerAccountResult), "Computers", "Delete");

            #endregion

            #region Groups Response Types

            // GetGroupInfo Response Type Config.
            config.SetActualResponseType(typeof(GroupInfoResult), "Groups", "Get");

            #endregion

            #region Users Response Types

            // GetUserInfo Response Type Config.
            config.SetActualResponseType(typeof(UserInfoResult), "Users", "Get");

            // IsMemberOf Response Type Config.
            config.SetActualResponseType(typeof(MembershipResult), "Users", "IsMemberOf");

            // UserGroupMembership Response Type Config.
            config.SetActualResponseType(typeof(UserMembershipResult), "Users", "UserGroupMembership");
            
            // CheckADObject Response Type Config.
            config.SetActualResponseType(typeof(ADObjectCheckResult), "Users", "CheckADObject");

            #endregion
        }

#if Handle_PageResultOfT
        private static object GeneratePageResult(HelpPageSampleGenerator sampleGenerator, Type type)
        {
            if (type.IsGenericType)
            {
                Type openGenericType = type.GetGenericTypeDefinition();
                if (openGenericType == typeof(PageResult<>))
                {
                    // Get the T in PageResult<T>
                    Type[] typeParameters = type.GetGenericArguments();
                    Debug.Assert(typeParameters.Length == 1);

                    // Create an enumeration to pass as the first parameter to the PageResult<T> constuctor
                    Type itemsType = typeof(List<>).MakeGenericType(typeParameters);
                    object items = sampleGenerator.GetSampleObject(itemsType);

                    // Fill in the other information needed to invoke the PageResult<T> constuctor
                    Type[] parameterTypes = new Type[] { itemsType, typeof(Uri), typeof(long?), };
                    object[] parameters = new object[] { items, null, (long)ObjectGenerator.DefaultCollectionSize, };

                    // Call PageResult(IEnumerable<T> items, Uri nextPageLink, long? count) constructor
                    ConstructorInfo constructor = type.GetConstructor(parameterTypes);
                    return constructor.Invoke(parameters);
                }
            }

            return null;
        }
#endif
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WindAPI.Classes.ResultObjects;
using WindAPI.Config;

namespace WindAPI.Handlers
{
    /// <summary>
    /// This class handles checks for permissions made against the Checkpoint API.
    /// </summary>
    public static class CheckpointAuth
    {
        /// <summary>
        /// This method receives an accessToken and checks it against the Checkpoint API.
        /// </summary>
        /// <param name="accessToken">A Checkpoint Access Token.</param>
        /// <returns>true if the access token is valid, false otherwise.</returns>
        public static bool CheckCheckpointToken(string accessToken)
        {
            bool isAuth = false;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.checkpointAPIURL);
                var authResponse = client.GetAsync($"me?access_token={accessToken}").Result;
                CheckpointAuthResponse checkpointAuth = authResponse.Content.ReadAsAsync<CheckpointAuthResponse>().Result;
                if (authResponse.IsSuccessStatusCode && checkpointAuth.isAdmin)
                {
                    isAuth = true;
                }
            }

            return isAuth;
        }
    }
}
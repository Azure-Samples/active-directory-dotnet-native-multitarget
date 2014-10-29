using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DirectorySearcherLib
{
    public static class DirectorySearcher
    {
        public static string clientId = "e11a0451-ac9d-4c89-afd8-d2fa3322ef68";
        public static string authority = "https://login.windows.net/common";
        public static Uri returnUri = new Uri("http://li");        
        const string graphResourceUri = "https://graph.windows.net";
        public static string graphApiVersion = "2013-11-08";

        public static async Task<List<User>> SearchByAlias(string alias, IAuthorizationParameters parent) // add this param
        {
            AuthenticationResult authResult = null;
            JObject jResult = null;
            List<User> results = new List<User>();;
            try
            {
                AuthenticationContext authContext = new AuthenticationContext(authority);
                authResult = await authContext.AcquireTokenAsync(graphResourceUri, clientId, returnUri, parent);
            }
            catch (Exception ee)
            {
                results.Add(new User { error = ee.Message });
                return results;
            }

            try
            {
                string graphRequest = String.Format(CultureInfo.InvariantCulture, "{0}/{1}/users?api-version={2}&$filter=mailNickname eq '{3}'", graphResourceUri, authResult.TenantId, graphApiVersion, alias);
                //string graphRequest = String.Format(CultureInfo.InvariantCulture, "{0}/{1}/users?api-version={2}&$filter=startswith(mailNickname, '{3}')", graphResourceUri, authResult.TenantId, graphApiVersion, alias);
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, graphRequest);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                HttpResponseMessage response = await client.SendAsync(request);

                string content = await response.Content.ReadAsStringAsync();
                jResult = JObject.Parse(content);
            }
            catch (Exception ee)
            {
                results.Add(new User { error = ee.Message });
                return results;
            }

            if (jResult["odata.error"] != null)
            {
                results.Add(new User { error = (string)jResult["odata.error"]["message"]["value"] });
                return results;
            }
            if (jResult["value"] == null)
            {
                results.Add(new User { error = "Unknown Error." });
                return results;
            }
            foreach (JObject result in jResult["value"])
            {
                results.Add(new User
                {
                    displayName = (string)result["displayName"],
                    givenName = (string)result["givenName"],
                    surname = (string)result["surname"],
                    userPrincipalName = (string)result["userPrincipalName"],
                    telephoneNumber = (string)result["telephoneNumber"] == null ? "Not Listed." : (string)result["telephoneNumber"]
                });
            }

            return results;
        }
    }
}
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

        public static async Task<User> SearchByAlias(string alias, IAuthorizationParameters parent) // add this param
        {
            AuthenticationContext authContext = new AuthenticationContext(authority);
            AuthenticationResult authResult = null;
            User result = null;
            try
            {
                authResult = await authContext.AcquireTokenAsync(graphResourceUri, clientId, returnUri, parent);
            }
            catch (Exception ee)
            {
                return new User { error = new OdataError { code = "MyCode", message = new Message { lang = "en", value = "Error on Acquire Token" } } };
            }

            try
            {
                string graphRequest = String.Format(CultureInfo.InvariantCulture, "{0}/{1}/users?api-version={2}&$filter=mailNickname eq '{3}'", graphResourceUri, authResult.TenantId, graphApiVersion, alias);
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, graphRequest);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                HttpResponseMessage response = await client.SendAsync(request);

                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<User>(content);

            }

            catch (Exception ee)
            {
                return new User { error = new OdataError { code = "MyCode", message = new Message { lang = "en", value = "Error on GraphAPI Call" } } };
            }

            return result;

        }
    }
}
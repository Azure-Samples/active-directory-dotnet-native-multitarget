using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<string> SearchByAlias(string alias, IAuthorizationParameters parent) // add this param
        {
            AuthenticationContext ac = new AuthenticationContext(authority);
            AuthenticationResult ar = null;
            try
            {
                ar = await ac.AcquireTokenAsync(graphResourceUri, clientId, returnUri, parent);
            }
            catch (Exception ee)
            {
                return ee.Message;
            }

            return ar.AccessToken;                
        }
    }
}

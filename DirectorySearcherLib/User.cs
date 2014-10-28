using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorySearcherLib
{
    public class User
    {
        [JsonProperty(PropertyName = "odata.metadata")]
        public string metadata { get; set; }
        [JsonProperty(PropertyName = "odata.error")]
        public OdataError error { get; set; }
        public List<Value> value { get; set; }
    }
    public class Message
    {
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class OdataError
    {
        public string code { get; set; }
        public Message message { get; set; }
    }

    public class Value
    {
        [JsonProperty(PropertyName = "odata.type")]
        public string type { get; set; }
        public string objectType { get; set; }
        public string objectId { get; set; }
        public bool accountEnabled { get; set; }
        public List<string> assignedLicenses { get; set; }
        public List<string> assignedPlans { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string department { get; set; }
        public string dirSyncEnabled { get; set; }
        public string displayName { get; set; }
        public string facsimileTelephoneNumber { get; set; }
        public string givenName { get; set; }
        public string immutableId { get; set; }
        public string jobTitle { get; set; }
        public string lastDirSyncTime { get; set; }
        public string mail { get; set; }
        public string mailNickname { get; set; }
        public string mobile { get; set; }
        public List<string> otherMails { get; set; }
        public string passwordPolicies { get; set; }
        public string passwordProfile { get; set; }
        public string physicalDeliveryOfficeName { get; set; }
        public string postalCode { get; set; }
        public string preferredLanguage { get; set; }
        public List<string> provisionedPlans { get; set; }
        public List<string> provisioningErrors { get; set; }
        public List<string> proxyAddresses { get; set; }
        public string state { get; set; }
        public string streetAddress { get; set; }
        public string surname { get; set; }
        public string telephoneNumber { get; set; }
        public string usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public string userType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Windows;

namespace smallSmiteApp.APIcalls
{
    public class SmiteAPI
    {
        string DevID = "2193";
        string AuthKey = "A94348A93B544511952BAA38CE96CA8B";
        string baseURL = "http://api.smitegame.com/smiteapi.svc/";
        string timeStamp, signature,sessionID;
       public SmiteAPI()
        {
             timeStamp = CreateTimeStamp();
             signature = CreateSignature("createsession");
             CreateSession();

        }

        private void CreateSession()
        {
            string sessionIdURL = baseURL + $"createsessionjson/{DevID}/{signature}/{timeStamp}";
            var responseFromServer = ApiCall(sessionIdURL);
            using (var web = new WebClient())
            {
                web.Encoding = System.Text.Encoding.UTF8;
                var jsonString = responseFromServer;
                var jss = new JavaScriptSerializer();
                var g = jss.Deserialize<SessionInfo>(jsonString);
                sessionID = g.session_id;
            }

        }

        public string ApiCall(string urlParameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);

            // Add an Accept header for JSON or XML format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                return (int)response.StatusCode + response.ReasonPhrase;
            }
        }


        public string ConstructRequestString(string endPointName,string other)
        {
            if (endPointName.Equals("pingjson"))
                return endPointName;
            signature = CreateSignature(DevID + endPointName + AuthKey + timeStamp+other);
            string suffix= endPointName+"/"+DevID+"/"+signature+"/"+sessionID+"/"+timeStamp;
            if (endPointName.Equals("getgodsjson"))
            {
                suffix += "/1";
            }
            return suffix;
        }

        public string Excecute(string endPointname)
        {
           string apiCallResult= ApiCall(ConstructRequestString(endPointname, null));

            switch (endPointname)
            {
                case "pingjson":
                    return apiCallResult;
                case "getgodsjson":
                    using (var web = new WebClient())
                    {
                        web.Encoding = System.Text.Encoding.UTF8;
                        var jsonString = apiCallResult;
                        var jss = new JavaScriptSerializer();
                        var GodsList = jss.Deserialize<List<Gods>>(jsonString);
                        string GodsListStr = "";

                        foreach (Gods x in GodsList)
                            GodsListStr = GodsListStr + ", " + x.Name;

                        return "Here are the Gods: " + GodsListStr;
                    }
                    break;
                default:
                    return apiCallResult;
                    break;
            }
        }
        public string Ping()
        {
            string retValue = baseURL + "pingjson";
            return retValue;
        }

        private string CreateSignature(string input)
        {
            string signatureString = DevID + input + AuthKey + timeStamp;
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bytes = System.Text.Encoding.UTF8.GetBytes(signatureString);
            bytes = md5.ComputeHash(bytes);
            var sb = new System.Text.StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();

        }


        private string CreateTimeStamp()
        {
            return DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            
        }
        #region objects

        public class SessionInfo
        {
            public string ret_msg { get; set; }
            public string session_id { get; set; }
            public string timestamp { get; set; }

        }
        public class Menuitem
        {
            public string description { get; set; }
            public string value { get; set; }
        }

        public class Rankitem
        {
            public string description { get; set; }
            public string value { get; set; }
        }

        public class AbilityDescription
        {
            public string description { get; set; }
            public string secondaryDescription { get; set; }
            public List<Menuitem> menuitems { get; set; }
            public List<Rankitem> rankitems { get; set; }
            public string cooldown { get; set; }
            public string cost { get; set; }
        }

        public class AbilityRoot
        {
            public AbilityDescription itemDescription { get; set; }
        }

        public class Gods
        {
            public int abilityId1 { get; set; }
            public int abilityId2 { get; set; }
            public int abilityId3 { get; set; }
            public int abilityId4 { get; set; }
            public int abilityId5 { get; set; }
            public AbilityRoot abilityDescription1 { get; set; }
            public AbilityRoot abilityDescription2 { get; set; }
            public AbilityRoot abilityDescription3 { get; set; }
            public AbilityRoot abilityDescription4 { get; set; }
            public AbilityRoot abilityDescription5 { get; set; }
            public int id { get; set; }
            public string Pros { get; set; }
            public string Type { get; set; }
            public string Roles { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            public string OnFreeRotation { get; set; }
            public string Lore { get; set; }
            public int Health { get; set; }
            public Double HealthPerLevel { get; set; }
            public Double Speed { get; set; }
            public Double HealthPerFive { get; set; }
            public Double HP5PerLevel { get; set; }
            public Double Mana { get; set; }
            public Double ManaPerLevel { get; set; }
            public Double ManaPerFive { get; set; }
            public Double MP5PerLevel { get; set; }
            public Double PhysicalProtection { get; set; }
            public Double PhysicalProtectionPerLevel { get; set; }
            public Double MagicProtection { get; set; }
            public Double MagicProtectionPerLevel { get; set; }
            public Double PhysicalPower { get; set; }
            public Double PhysicalPowerPerLevel { get; set; }
            public Double AttackSpeed { get; set; }
            public double AttackSpeedPerLevel { get; set; }
            public string Pantheon { get; set; }
            public string Ability1 { get; set; }
            public string Ability2 { get; set; }
            public string Ability3 { get; set; }
            public string Ability4 { get; set; }
            public string Ability5 { get; set; }
            public string Item1 { get; set; }
            public string Item2 { get; set; }
            public string Item3 { get; set; }
            public string Item4 { get; set; }
            public string Item5 { get; set; }
            public string Item6 { get; set; }
            public string Item7 { get; set; }
            public string Item8 { get; set; }
            public string Item9 { get; set; }
            public int ItemId1 { get; set; }
            public int ItemId2 { get; set; }
            public int ItemId3 { get; set; }
            public int ItemId4 { get; set; }
            public int ItemId5 { get; set; }
            public int ItemId6 { get; set; }
            public int ItemId7 { get; set; }
            public int ItemId8 { get; set; }
            public int ItemId9 { get; set; }
            public string ret_msg { get; set; }
        } 
        #endregion objects

    }
}

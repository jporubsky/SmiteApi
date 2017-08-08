using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace smallSmiteApp.APIcalls
{
    public class SmiteAPI
    {
        string sessionID;
        string DevID = "2193";
        string AuthKey = "A94348A93B544511952BAA38CE96CA8B";
        string baseURL = "http://api.smitegame.com/smiteapi.svc/";
        SmiteAPI()
        {
            sessionID = baseURL + $"createsessionxml /{ DevID}/{ signature}/{ timestamp}";
        }

        public string ApiCall(string urlParameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/xml"));

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

        public string Ping()
        {
            string retValue= baseURL + "pingxml";
            return retValue;
        }

        private string CreateSignature()
        {
            return "";
        }
    }
}

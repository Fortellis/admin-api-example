using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using AdminAPIImplementation.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace AdminAPIImplementation.Services
{
    public class FortellisConnectionCallbackService : IFortellisConnectionCallbackService
    {
        public IConfiguration Configuration { get; }

        public FortellisConnectionCallbackService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string getFortellisToken()
        {
            string tokenUrl = String.Format("https://{0}/oauth2/{1}/v1/token", 
                Configuration["Fortellis:AuthorizationServerDomain"],
                Configuration["Fortellis:AuthorizationServerId"]);

            var http = new RestClient(tokenUrl);
            var req = new RestRequest(Method.POST);
            buildParamsForTokenRequest(req);

            var res = http.Execute(req);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"{res.Content.ToString()}. Make sure you have valid apikey/secret in appsettings.jsons");
            }
            return JsonConvert.DeserializeObject<FortellisTokenResponse>(res.Content).access_token;
        }

        private string getBasicAuthForTokenRequest()
        {
            string APIKey = Configuration["Fortellis:ProviderAPIKey"];
            string APISecret = Configuration["Fortellis:ProviderAPISecret"];
            string credentials = String.Format("{0}:{1}", APIKey, APISecret);
            byte[] bytes = Encoding.ASCII.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            return String.Concat("Basic ", base64);
        }

        private void buildParamsForTokenRequest(IRestRequest req)
        {
            req.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
            req.AddParameter("scope", "anonymous", ParameterType.GetOrPost);
            req.AddHeader("Authorization", getBasicAuthForTokenRequest());
        }



        public void approveFortellisActivationRequest(string accessToken, string connectionId, string status)
        {
            string connectionCallbackURI = String.Format("https://{0}/connections/{1}/callback", Configuration["Fortellis:ConnectionCallbackDomain"], connectionId);
            CallbackRequest fortellisCallbackRequest = new CallbackRequest(status);

            var http = new RestClient(connectionCallbackURI);
            var req = new RestRequest(Method.POST)
                .AddHeader("Authorization", $"Bearer {accessToken}")
                .AddJsonBody(fortellisCallbackRequest);

            var res = http.Execute(req);
            if(res.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(res.Content.ToString());
            }
            Console.WriteLine("Connection activation successful.");
        }
    }
}

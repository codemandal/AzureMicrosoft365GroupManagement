using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace MicrosoftGroupManagement
{
    public static class AddMemberAndOwner
    {
        [FunctionName("AddMemberAndOwner")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string OId = req.Query["OId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            OId = OId ?? data?.OId;

            var clientId = "YOUR_CLIENT_ID";
            var tenantId = "YOUR_TENANT_ID";
            var clientSecret = "YOUR_CLIENT_SECRET";
            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            var graphClient = new GraphServiceClient(clientSecretCredential);
            string REady_STAR_Report = "OBJECT_ID";

            string responseMessage = string.IsNullOrEmpty(OId)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {OId}. This HTTP triggered function executed successfully.";


            var requestBodyMember = new ReferenceCreate
            {
                OdataId = "https://graph.microsoft.com/v1.0/directoryObjects/" + OId,
            };
            await graphClient.Groups[REady_STAR_Report].Members.Ref.PostAsync(requestBodyMember);
            var requestBodyOwner = new ReferenceCreate
            {
                OdataId = "https://graph.microsoft.com/v1.0/users/" + OId,
            };


            await graphClient.Groups[REady_STAR_Report].Owners.Ref.PostAsync(requestBodyOwner);

            return new OkObjectResult(responseMessage);
        }
    }
}

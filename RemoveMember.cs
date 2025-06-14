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
using System.Collections.Generic;

namespace MicrosoftGroupManagement
{
    public static class RemoveMember
    {
        [FunctionName("RemoveMember")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var clientId = "YOUR_CLIENT_ID";
                var tenantId = "YOUR_TENANT_ID";
                var clientSecret = "YOUR_CLIENT_SECRET";
                var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

                var graphClient = new GraphServiceClient(clientSecretCredential);
                string REady_STAR_Report = "OBJID";
              
                // Get groups
                //var groups = await graphClient.Groups.GetAsync();

                List<string> grpIds = new List<string>();
                grpIds.Add(REady_STAR_Report);
                List<string> removeUsers = new List<string>();
             
                //for each group add members
                foreach (string u in removeUsers)
                {
                    await graphClient.Groups[REady_STAR_Report].Members[u].Ref.DeleteAsync();
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return new OkObjectResult("Members removed successfully!");

        }
    }
}

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
using Microsoft.Graph.Models;

namespace MicrosoftGroupManagement
{
    public static class CreateChannel
    {
        [FunctionName("CreateChannel")]
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
               
                
                var teamId = "TEAM_ID";

                

                var requestBody = new Channel
                {
                    DisplayName = "First Created Channel",
                    Description = "This channel is where we debate all future architecture plans",
                    MembershipType = ChannelMembershipType.Standard,
                };

                // To initialize your graphClient, see https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp
                var result = await graphClient.Teams[teamId].Channels.PostAsync(requestBody);




            }
            catch (Exception e)
            {

                throw;
            }

            return new OkObjectResult("Members added successfully!");
        }
    }
}

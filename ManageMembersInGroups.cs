using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Graph;
using System.Linq;
using Microsoft.Graph.Models;
using System.Collections.Generic;

namespace MicrosoftGroupManagement
{
    public static class ManageMembersInGroups
    {
        [FunctionName("ManageMembersInGroups")]
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
                string REady_STAR_Report = "ObjectID";
            
                // Get groups
                var groups = await graphClient.Groups.GetAsync();

                List<string> grpIds = new List<string>();
                grpIds.Add(REady_STAR_Report);
           
                string userOjID = "ObjectIDofUser"
                //for each group add members

                foreach (var g in grpIds)
                { 
                   // var users = graphClient.Users.GetAsync().Result.Value.Select(u=> u.Id);
                   
                    var members = await graphClient.Groups[g].Members.GetAsync();
                    log.LogInformation("Group Name:" + g);
                    var addUsers = new List<string>();
                  
                    // users.Except(members.Value.Select(m => m.Id)).ToList();
                    if (addUsers.Count > 0)
                    {
                        string url = "https://graph.microsoft.com/v1.0/directoryObjects/";
                        int apiDirectoryMaxBatchSize = 20;
                        for (int i = 0; i < addUsers.Count(); i = i + apiDirectoryMaxBatchSize)
                        {

                            var requestBody = new Group
                            {
                                AdditionalData = new Dictionary<string, object>
                                {
                                    {
                                        "members@odata.bind" , addUsers.Skip(i).Take(apiDirectoryMaxBatchSize).Select(u => $"{url}{u}")
                                    },
                                },
                            };

                            await graphClient.Groups[g].PatchAsync(requestBody);

                        }

                        var requestBodyOwner = new ReferenceCreate
                        {
                            OdataId = "https://graph.microsoft.com/v1.0/users/" + userOjID,
                        };


                        await graphClient.Groups[g].Owners.Ref.PostAsync(requestBodyOwner);

                    }
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return new OkObjectResult("Members added successfully!");
        
        }
    }
}

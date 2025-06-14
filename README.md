📁 Azure Function – Microsoft 365 Group Manager
This project is a sample Azure Function App designed to manage Microsoft 365 Groups. It demonstrates how to use Azure Functions with the Microsoft Graph API to automate group creation, updates, deletions, and membership management.

🔧 Prerequisites
Visual Studio 2022
.NET 6.0 or later
Azure Functions Tools for Visual Studio
Azure Subscription
Microsoft 365 Developer Account
Registered Azure AD App with appropriate Microsoft Graph API permissions
Secrets stored in Azure Key Vault or local.settings.json

🚀 Features
Create new Microsoft 365 Groups
Update group metadata (name, description, etc.)
Delete existing groups
Manage group members (add/remove)
Add Team Channel
Trigger types:
HTTP Trigger
Timer Trigger (optional for scheduled tasks)

🛠️ Technologies Used
Azure Functions (.NET isolated or in-process)
Microsoft Graph SDK
Azure Identity & MSAL Authentication
Azure Key Vault (for secure secret management)
Dependency Injection
Logging via ILogger

🔐 Configuration
Update the following settings in local.settings.json for local development:
json
Copy
Edit
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "TenantId": "<your-tenant-id>",
    "ClientId": "<your-app-client-id>",
    "ClientSecret": "<your-client-secret>",
    "GraphScopes": "https://graph.microsoft.com/.default"
  }
}
⚠️ Never commit secrets to source control. Use Azure Key Vault in production.

🧪 How to Run
Clone the repository.
Open the solution in Visual Studio 2022.
Update local.settings.json with your Azure AD app credentials.
Press F5 to run locally.
Use tools like Postman or curl to hit HTTP endpoints.

🧾 Sample HTTP Request (Create Group)
http
Copy
Edit
POST /api/CreateGroup
Content-Type: application/json

{
  "displayName": "Test Group",
  "description": "Group created via Azure Function",
  "mailNickname": "testgroup",
  "owners": ["user1@domain.com"],
  "members": ["user2@domain.com"]
}
📚 Resources
Azure Functions Documentation

Microsoft Graph .NET SDK

Azure Identity Authentication

📄 License
This project is licensed under the MIT License. See LICENSE for details.

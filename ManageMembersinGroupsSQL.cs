using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace MicrosoftGroupManagement
{
    public  class ManageMembersinGroupsSQL
    {
        private  readonly ILogger _logger;

        public ManageMembersinGroupsSQL(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ManageMembersinGroupsSQL>();
        }
        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [FunctionName("ManageMembersinGroupsSQL")]
        public static void Run(
                [SqlTrigger("Name_Of_Trigger", "Name_Of_Database")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                ILogger logger)
        {
            foreach (var change in changes)
            {
                var toDoItem = change.Item;
                logger.LogInformation($"Change operation: {change.Operation}");
                logger.LogInformation($"Id: {toDoItem.Id}, Title: {toDoItem.title}, Completed: {toDoItem.completed}");
            }

        }
    }

    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public bool? completed { get; set; }
    }
}

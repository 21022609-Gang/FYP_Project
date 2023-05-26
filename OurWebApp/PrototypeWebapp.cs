using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;

namespace MyNamespace
{
    public static class MyFunction
    {
        [FunctionName("Home")]
        public static IActionResult Home(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Home function processed a request.");

            string responseMessage = "<html><body><h1>Welcome to my Azure Functions web app!</h1></body></html>";

            return new ContentResult
            {
                Content = responseMessage,
                ContentType = "text/html",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [FunctionName("Hello")]
        public static IActionResult Hello(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "hello/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            log.LogInformation($"Hello function processed a request for {name}.");

            string responseMessage = $"<html><body><h1>Hello, {name}!</h1></body></html>";

            return new ContentResult
            {
                Content = responseMessage,
                ContentType = "text/html",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}

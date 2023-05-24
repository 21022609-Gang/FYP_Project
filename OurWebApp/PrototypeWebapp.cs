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
        [FunctionName("MyFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "<html><body><h1>Hello World!</h1></body></html>";

            return new ContentResult
            {
                Content = responseMessage,
                ContentType = "text/html",
                StatusCode = 200
            };
        }
    }
}
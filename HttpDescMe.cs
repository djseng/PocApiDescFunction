using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PocApiDescFunction
{
    public class HttpDescMe
    {
        private readonly ILogger<HttpDescMe> _logger;

        public HttpDescMe(ILogger<HttpDescMe> logger)
        {
            _logger = logger;
        }

        [Function("HttpDescMe")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}

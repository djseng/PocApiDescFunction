using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums; // Add this using directive

namespace PocApiDescFunction;

public class HttpHelloMe
{
    private readonly ILogger<HttpHelloMe> _logger;

    public HttpHelloMe(ILogger<HttpHelloMe> logger)
    {
        _logger = logger;
    }

    [OpenApiOperation(operationId: "greeting", tags: ["greeting"], Summary = "Greetings", Description = "This shows a welcome message.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Hello), Description = "Returns a greeting message.")]
    [Function(nameof(HttpHelloMe))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "hello/{name:alpha}")]
        HttpRequest req, string name)
    {
        _logger.LogInformation("Going to say hello to {name}.", name);
        return new OkObjectResult(new Hello($"Hello, {name}!"));
    }
}

public record Hello(string Message);
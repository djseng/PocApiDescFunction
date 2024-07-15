using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace PocApiDescFunction;

public class HttpHelloMe
{
    private readonly ILogger<HttpHelloMe> _logger;

    public HttpHelloMe(ILogger<HttpHelloMe> logger)
    {
        _logger = logger;
    }

    [Function(nameof(HttpHelloMe))]
    [OpenApiOperation(operationId: "Greeting", tags: ["GM"])]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: MediaTypeNames.Application.Json,
        bodyType: typeof(Hello),
        Summary = "The GM",
        Description = "It is always a great day to have a great day!")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "hello/{name:alpha}")]
        HttpRequest req, string name)
    {
        _logger.LogInformation("Going to say GM to {name}.", name);
        return new OkObjectResult(new Hello($"GM, {name}!"));
    }
}

public record Hello(string Message);
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
    [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = true, Type = typeof(string),
        Description = "The name of the person.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: MediaTypeNames.Application.Json,
        bodyType: typeof(Hello),
        Description = "It is always a great day to have a great day!")]
    [OpenApiResponseWithoutBody(
        statusCode: HttpStatusCode.NotFound,
        Description = "Non alpha name is not allowed.")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "hello/{name:alpha}")]
        HttpRequest req, string name)
    {
        _logger.LogInformation("Going to say GM to {name}.", name);
        return new OkObjectResult(new Hello(name));
    }
}

public record Hello(string Name) 
{
    public string Message { get; } = $"GM, {Name}!";
};
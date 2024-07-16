using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

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
        Description = "The name of the person.",
        Example = typeof(NameOpenApiExample))]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: MediaTypeNames.Application.Json,
        bodyType: typeof(Hello),
        Description = "It is always a great day to have a great day!",
        Example = typeof(HelloOpenApiExample))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "hello/{name:alpha}")]
        HttpRequest req, string name)
    {
        _logger.LogInformation("Going to say GM to {name}.", name);
        return new OkObjectResult(new Hello($"GM, {name}!"));
    }
}

public class NameOpenApiExample : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy? namingStrategy = null)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("name", "John", namingStrategy));
        return this;
    }
}

public class HelloOpenApiExample : OpenApiExample<Hello>
{
    public override IOpenApiExample<Hello> Build(NamingStrategy? namingStrategy = null)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("200", "This is a great day!", new Hello("GM, John!"), namingStrategy));
        return this;
    }
}

public record Hello(string Message);
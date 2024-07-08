using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PocApiDescFunction;

public class HttpDescMe
{
    private readonly ILogger<HttpDescMe> _logger;

    public HttpDescMe(ILogger<HttpDescMe> logger)
    {
        _logger = logger;
    }

    [Function("HttpDescMe")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post",
            Route = "hello/{name:alpha}")]
        HttpRequest req, string name)
    {
        _logger.LogInformation("Going to say hello to {name}.", name);
        return new OkObjectResult(new { message = $"Hello, {name}!"});
    }
}

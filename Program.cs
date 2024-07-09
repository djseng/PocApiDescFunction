using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(
        worker => worker.UseNewtonsoftJson())
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.RemoveAll(typeof(IHostedService));
    })
    .Build();

host.Run();



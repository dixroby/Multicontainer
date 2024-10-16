using Microsoft.Extensions.Caching.Distributed;

namespace WebAPI.Controllers;

public static class CounterController
{
    const string Key = "Counter";

    public static IEndpointRouteBuilder UseCounterEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("counter",
            (ILoggerFactory loggerFactory, IDistributedCache cache) =>
            {
                ILogger Logger = loggerFactory.CreateLogger("CounterController");

                string Result = null;

                try
                {
                    string CounterValue = cache.GetString(Key);
                    if (int.TryParse(CounterValue, out int counter))
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                    }

                    Result = counter.ToString();
                    cache.SetString(Key, Result);
                }
                catch (Exception ex)
                {
                    Result = "Redis";
                    Logger.LogError(Result);

                }
                return Results.Ok(Result);
            });

        return builder;
    }
}

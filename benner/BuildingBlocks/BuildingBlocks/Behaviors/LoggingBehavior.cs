namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[INÍCIO] Requisição em andamento={request} - Resposta da requisição={Response} - Dados da requisição={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();

        timer.Start();

        var response = await next();

        timer.Stop();
        var timeElapsed = timer.Elapsed;

        if (timeElapsed.Seconds > 3)
            logger.LogWarning("[PERFORMANCE - CUIDADO] A requisição {Request} levou {timeElapsed} segundos para ser executada.",
                typeof(TRequest).Name, timeElapsed.Seconds);

        logger.LogInformation("[FIM] Término da requisiçao {Request} com {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}

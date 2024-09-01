namespace BuildingBlocks.Behaviors;

/// <summary>
/// Comportamento de log para cada requisição e resposta na pipeline do mediatR
/// </summary>
/// <typeparam name="TRequest">O tipo da requisição que implementa a interface</typeparam>
/// <typeparam name="TResponse">O tipo da resposta esperada</typeparam>
/// <param name="logger"></param>
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
{
    // Método para manipular o log
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[INÍCIO] Requisição em andamento={request} - Resposta da requisição={Response} - Dados da requisição={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        // Inicia a contagem da execução
        var timer = new Stopwatch();

        timer.Start();

        var response = await next();

        timer.Stop();
        var timeElapsed = timer.Elapsed;

        // Verifica se o tempo de execução foi superior a 3 segundos, se sim, faz um aviso de performance
        if (timeElapsed.Seconds > 3)
            logger.LogWarning("[PERFORMANCE - CUIDADO] A requisição {Request} levou {timeElapsed} segundos para ser executada.",
                typeof(TRequest).Name, timeElapsed.Seconds);

        logger.LogInformation("[FIM] Término da requisiçao {Request} com {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}

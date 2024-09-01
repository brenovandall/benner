namespace Parking.API.Parking.Estacionamento.CreateEstacionamento;

public record CreateEstacionamentoRequest(DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId);
public record CreateEstacionamentoResponse(Guid Id);

/// <summary>
/// Endpoint para criar um novo estacionamento (voltado para registro de entrada e veículo)
/// </summary>
public class CreateEstacionamentoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("estacionamento", async (CreateEstacionamentoRequest request, ISender sender) =>
        {
            // Faz o mapeamento dos parametros
            var command = request.Adapt<CreateEstacionamentoCommand>();

            // Envia o comando através do MediatR
            var result = await sender.Send(command);

            // Faz o mapeamento do resultado
            var response = result.Adapt<CreateEstacionamentoResponse>();

            return Results.Created($"/estacionamento/{response.Id}", response);
        })
        .WithName("PostEstacionamento")
        .Produces<CreateEstacionamentoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Adicionar um estacionamento")
        .WithDescription("Criar e adicionar um estacionamento");
    }
}

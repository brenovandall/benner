namespace Parking.API.Parking.Estacionamento.GetEstacionamento;

// public record GetEstacionamentoRequest();
public record GetEstacionamentoResponse(IReadOnlyList<Models.Estacionamento> Estacionamentos);

/// <summary>
/// Endpoint para listar todos os estacionamentos
/// </summary>
public class GetEstacionamentoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("estacionamento", async (ISender sender) =>
        {
            // Envia o comando através do MediatR
            var result = await sender.Send(new GetEstacionamentoQuery());

            // Faz o mapeamento do resultado
            var response = result.Adapt<GetEstacionamentoResponse>();

            return Results.Ok(response);
        })
        .WithName("GetEstacionamento")
        .Produces<GetEstacionamentoResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Lista de estacionamentos")
        .WithDescription("Obter listagem de estacionamentos");
    }
}


namespace Parking.API.Parking.Estacionamento.GetEstacionamentoById;

// public record GetEstacionamentoByIdRequest();
public record GetEstacionamentoByIdResponse(Models.Estacionamento Estacionamento);

/// <summary>
/// Endpoint para obter um estacionamento com o ID passado no router
/// </summary>
public class GetEstacionamentoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/estacionamento/{id}", async (Guid id, ISender sender) =>
        {
            // Envia o comando através do MediatR com o valor do ID
            var result = await sender.Send(new GetEstacionamentoByIdQuery(id));

            // Faz o mapeamento do resultado
            var response = result.Adapt<GetEstacionamentoByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetEstacionamentoById")
        .Produces<GetEstacionamentoByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Estacionamento por Id")
        .WithDescription("Buscar estacionamento através do Id");
    }
}

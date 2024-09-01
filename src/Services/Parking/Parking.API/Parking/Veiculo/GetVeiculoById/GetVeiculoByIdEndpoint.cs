namespace Parking.API.Parking.Veiculo.GetVeiculoById;

// public record GetVeiculoByIdRequest();
public record GetVeiculoByIdResponse(Models.Veiculo Veiculo);

/// <summary>
/// Endpoint para obter um veículo pelo ID
/// </summary>
public class GetVeiculoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/veiculo/{id}", async (Guid id, ISender sender) =>
        {
            // Envia o comando através do MediatR com o ID informado
            var result = await sender.Send(new GetVeiculoByIdQuery(id));

            // Faz o mapeamento do resultado
            var response = result.Adapt<GetVeiculoByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetVeiculoById")
        .Produces<GetVeiculoByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Veiculo por Id")
        .WithDescription("Buscar veiculo através do Id");
    }
}

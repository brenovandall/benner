namespace Parking.API.Parking.Veiculo.GetVeiculoById;

// public record GetVeiculoByIdRequest();
public record GetVeiculoByIdResponse(Models.Veiculo Veiculo);

public class GetVeiculoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/veiculo/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetVeiculoByIdQuery(id));

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

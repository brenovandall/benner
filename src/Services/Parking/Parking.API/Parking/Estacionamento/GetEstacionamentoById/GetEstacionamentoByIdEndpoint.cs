
namespace Parking.API.Parking.Estacionamento.GetEstacionamentoById;

// public record GetEstacionamentoByIdRequest();
public record GetEstacionamentoByIdResponse(Models.Estacionamento Estacionamento);

public class GetEstacionamentoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/estacionamento/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetEstacionamentoByIdQuery(id));

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

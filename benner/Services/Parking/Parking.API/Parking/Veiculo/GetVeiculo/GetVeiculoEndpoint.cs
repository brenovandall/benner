namespace Parking.API.Parking.Veiculo.GetVeiculo;

// public record GetVeiculoRequest();
public record GetVeiculoResponse(IReadOnlyList<Models.Veiculo> Veiculos);

public class GetVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("veiculo", async (ISender sender) =>
        {
            var result = await sender.Send(new GetVeiculoQuery());

            var response = result.Adapt<GetVeiculoResponse>();

            return Results.Ok(response);
        })
        .WithName("GetVeiculo")
        .Produces<GetVeiculoResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Lista de veiculos")
        .WithDescription("Obter listagem de veiculos");
    }
}


namespace Parking.API.Parking.Estacionamento.UpdateEstacionamento;

public record UpdateEstacionamentoRequest(Guid Id, DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId)
    : ICommand<UpdateEstacionamentoResult>;
public record UpdateEstacionamentoResponse(bool IsSucess);

/// <summary>
/// Endpoint para editar um estacionamento (voltado para registro de saída)
/// </summary>
public class UpdateEstacionamentoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("estacionamento", async (UpdateEstacionamentoRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateEstacionamentoCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateEstacionamentoResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateEstacionamento")
        .Produces<UpdateEstacionamentoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Editar um estacionamento")
        .WithDescription("Editar um estacionamento");
    }
}

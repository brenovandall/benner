namespace Parking.API.Parking.Estacionamento.CreateEstacionamento;

public record CreateEstacionamentoRequest(DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId);
public record CreateEstacionamentoResponse(Guid Id);

public class CreateEstacionamentoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("estacionamento", async (CreateEstacionamentoRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateEstacionamentoCommand>();

            var result = await sender.Send(command);

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

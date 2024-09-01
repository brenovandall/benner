namespace Parking.API.Parking.Veiculo.CreateVeiculo;

public record CreateVeiculoRequest(string Placa, string Modelo, string Cor);
public record CreateVeiculoResponse(Guid Id);

/// <summary>
/// Endpoint para criar um novo veículo
/// </summary>
public class CreateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("veiculo", async (CreateVeiculoRequest request, ISender sender) =>
        {
            // Faz o mapeamento dos parametros
            var command = request.Adapt<CreateVeiculoCommand>();

            // Envia o comando através do MediatR
            var result = await sender.Send(command);

            // Faz o mapeamento do resultado
            var response = result.Adapt<CreateVeiculoResponse>();

            return Results.Created($"/veiculo/{response.Id}", response);
        })
        .WithName("CreateVeiculo")
        .Produces<CreateVeiculoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Adicionar um veiculo")
        .WithDescription("Criar e adicionar um veiculo");
    }
}

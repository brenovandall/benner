namespace Parking.API.Parking.Veiculo.GetVeiculo;

public record GetVeiculoQuery() : IQuery<GetVeiculoResult>;
public record GetVeiculoResult(IReadOnlyList<Models.Veiculo> Veiculos);

public class GetVeiculoQueryHandler (ParkingContext dbContext) 
    : IQueryHandler<GetVeiculoQuery, GetVeiculoResult>
{
    /// <summary>
    /// Método para listar todos os veículos
    /// </summary>
    public async Task<GetVeiculoResult> Handle(GetVeiculoQuery query, CancellationToken cancellationToken)
    {
        var veiculos = await dbContext.Veiculos
            .ToListAsync(cancellationToken);

        // Retorna a lista somente como leitura
        return new GetVeiculoResult(veiculos.AsReadOnly());
    }
}

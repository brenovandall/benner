namespace Parking.API.Parking.Veiculo.GetVeiculo;

public record GetVeiculoQuery() : IQuery<GetVeiculoResult>;
public record GetVeiculoResult(IReadOnlyList<Models.Veiculo> Veiculos);

public class GetVeiculoQueryHandler (ParkingContext dbContext) 
    : IQueryHandler<GetVeiculoQuery, GetVeiculoResult>
{
    public async Task<GetVeiculoResult> Handle(GetVeiculoQuery query, CancellationToken cancellationToken)
    {
        var veiculos = await dbContext.Veiculos
            .ToListAsync(cancellationToken);

        return new GetVeiculoResult(veiculos.AsReadOnly());
    }
}

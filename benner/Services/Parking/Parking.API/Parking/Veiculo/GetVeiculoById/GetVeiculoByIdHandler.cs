
namespace Parking.API.Parking.Veiculo.GetVeiculoById;

public record GetVeiculoByIdQuery(Guid Id)
    : IQuery<GetVeiculoByIdResult>;
public record GetVeiculoByIdResult(Models.Veiculo Veiculo);

public class GetVeiculoByIdHandler (ParkingContext dbContext) 
    : IQueryHandler<GetVeiculoByIdQuery, GetVeiculoByIdResult>
{
    public async Task<GetVeiculoByIdResult> Handle(GetVeiculoByIdQuery query, CancellationToken cancellationToken)
    {
        var veiculo = await dbContext.Veiculos
            .FirstOrDefaultAsync(x => x.Id == query.Id);

        if (veiculo == null)
            throw new EntidadeNaoEncontradaException(query.Id);

        return new GetVeiculoByIdResult(veiculo);
    }
}


namespace Parking.API.Parking.Veiculo.GetVeiculoById;

/// <param name="Id">Id para buscar no banco de dados</param>
public record GetVeiculoByIdQuery(Guid Id)
    : IQuery<GetVeiculoByIdResult>;
public record GetVeiculoByIdResult(Models.Veiculo Veiculo);

public class GetVeiculoByIdHandler (ParkingContext dbContext) 
    : IQueryHandler<GetVeiculoByIdQuery, GetVeiculoByIdResult>
{
    /// <summary>
    /// Método para buscar o veículo conforme o ID informado
    /// </summary>
    public async Task<GetVeiculoByIdResult> Handle(GetVeiculoByIdQuery query, CancellationToken cancellationToken)
    {
        var veiculo = await dbContext.Veiculos
            .FirstOrDefaultAsync(x => x.Id == query.Id);

        // Caso não encontrar o veículo, retorna uma exceção personalizada
        if (veiculo == null)
            throw new EntidadeNaoEncontradaException(query.Id);

        return new GetVeiculoByIdResult(veiculo);
    }
}

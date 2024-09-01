namespace Parking.API.Parking.Estacionamento.GetEstacionamentoById;

/// <param name="Id">Id para buscar no banco de dados</param>
public record GetEstacionamentoByIdQuery(Guid Id) 
    : IQuery<GetEstacionamentoByIdResult>;
public record GetEstacionamentoByIdResult(Models.Estacionamento Estacionamento);

public class GetEstacionamentoByIdQueryHandler (ParkingContext dbContext)
    : IQueryHandler<GetEstacionamentoByIdQuery, GetEstacionamentoByIdResult>
{
    /// <summary>
    /// Método para buscar o estacionamento
    /// </summary>
    public async Task<GetEstacionamentoByIdResult> Handle(GetEstacionamentoByIdQuery query, CancellationToken cancellationToken)
    {
        // Procura o estacionamento com o ID do parametro
        var estacionamento = await dbContext.Estacionamentos
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        // Se nao encontrar, retorna uma exceção personalizada
        if (estacionamento == null)
            throw new EntidadeNaoEncontradaException(query.Id);

        return new GetEstacionamentoByIdResult(estacionamento);
    }
}

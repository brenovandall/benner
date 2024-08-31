namespace Parking.API.Parking.Estacionamento.GetEstacionamentoById;

public record GetEstacionamentoByIdQuery(Guid Id) 
    : IQuery<GetEstacionamentoByIdResult>;
public record GetEstacionamentoByIdResult(Models.Estacionamento Estacionamento);

public class GetEstacionamentoByIdQueryHandler (ParkingContext dbContext)
    : IQueryHandler<GetEstacionamentoByIdQuery, GetEstacionamentoByIdResult>
{
    public async Task<GetEstacionamentoByIdResult> Handle(GetEstacionamentoByIdQuery query, CancellationToken cancellationToken)
    {
        var estacionamento = await dbContext.Estacionamentos
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (estacionamento == null)
            throw new EntidadeNaoEncontradaException(query.Id);

        return new GetEstacionamentoByIdResult(estacionamento);
    }
}

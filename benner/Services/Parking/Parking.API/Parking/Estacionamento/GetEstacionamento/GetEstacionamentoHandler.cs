namespace Parking.API.Parking.Estacionamento.GetEstacionamento;

public record GetEstacionamentoQuery() : IQuery<GetEstacionamentoResult>;
public record GetEstacionamentoResult(IReadOnlyList<Models.Estacionamento> Estacionamentos);

public class GetEstacionamentoQueryHandler (ParkingContext dbContext)
    : IQueryHandler<GetEstacionamentoQuery, GetEstacionamentoResult>
{
    public async Task<GetEstacionamentoResult> Handle(GetEstacionamentoQuery query, CancellationToken cancellationToken)
    {
        var estacionamentos = await dbContext.Estacionamentos
            .ToListAsync(cancellationToken);

        return new GetEstacionamentoResult(estacionamentos.AsReadOnly());
    }
}

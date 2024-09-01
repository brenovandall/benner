namespace Parking.API.Parking.Estacionamento.GetEstacionamento;

public record GetEstacionamentoQuery() : IQuery<GetEstacionamentoResult>;
public record GetEstacionamentoResult(IReadOnlyList<Models.Estacionamento> Estacionamentos);

public class GetEstacionamentoQueryHandler (ParkingContext dbContext)
    : IQueryHandler<GetEstacionamentoQuery, GetEstacionamentoResult>
{
    /// <summary>
    /// Método para listar os estacionamentos
    /// </summary>
    public async Task<GetEstacionamentoResult> Handle(GetEstacionamentoQuery query, CancellationToken cancellationToken)
    {
        // Faz a query para retornar todos os estacionamentos
        var estacionamentos = await dbContext.Estacionamentos
            .ToListAsync(cancellationToken);

        // Retorna a lista somente leitura
        return new GetEstacionamentoResult(estacionamentos.AsReadOnly());
    }
}

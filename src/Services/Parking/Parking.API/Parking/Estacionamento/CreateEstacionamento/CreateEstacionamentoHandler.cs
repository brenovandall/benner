namespace Parking.API.Parking.Estacionamento.CreateEstacionamento;

public record CreateEstacionamentoCommand(DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId)
    : ICommand<CreateEstacionamentoResult>;
public record CreateEstacionamentoResult(Guid Id);

/// <summary>
/// Validador para os parametros informados em <see cref="CreateEstacionamentoCommand"/>
/// </summary>
public class CreateEstacionamentoCommandValidator : AbstractValidator<CreateEstacionamentoCommand>
{
    public CreateEstacionamentoCommandValidator()
    {
        RuleFor(x => x.DataEntrada).NotEmpty().WithMessage("Data de entrada não pode ser nulo");
        RuleFor(x => x.VeiculoId).NotNull().WithMessage("O veículo não pode ser nulo");
    }
}

public class CreateEstacionamentoCommandHandler (ParkingContext dbContext) 
    : ICommandHandler<CreateEstacionamentoCommand, CreateEstacionamentoResult>
{
    /// <summary>
    /// Método para criar um novo estacionamento
    /// </summary>
    public async Task<CreateEstacionamentoResult> Handle(CreateEstacionamentoCommand command, CancellationToken cancellationToken)
    {
        var estacionamento = new Models.Estacionamento
        {
            DataEntrada = command.DataEntrada,
            DataSaida = null,
            ValorFinal = null,
            Duracao = command.Duracao,
            VeiculoId = command.VeiculoId,
        };

        await dbContext.Estacionamentos.AddAsync(estacionamento); // Adiciona o estacionamento no db
        await dbContext.SaveChangesAsync(cancellationToken); // Faz o commit no db

        // Retorna o ID do novo registro
        return new CreateEstacionamentoResult(estacionamento.Id);
    }
}

namespace Parking.API.Parking.Estacionamento.CreateEstacionamento;

public record CreateEstacionamentoCommand(DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId)
    : ICommand<CreateEstacionamentoResult>;
public record CreateEstacionamentoResult(Guid Id);

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

        await dbContext.Estacionamentos.AddAsync(estacionamento);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateEstacionamentoResult(estacionamento.Id);
    }
}

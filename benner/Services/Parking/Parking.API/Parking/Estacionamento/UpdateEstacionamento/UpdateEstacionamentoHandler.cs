
namespace Parking.API.Parking.Estacionamento.UpdateEstacionamento;

public record UpdateEstacionamentoCommand(Guid Id, DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId)
    : ICommand<UpdateEstacionamentoResult>;
public record UpdateEstacionamentoResult(bool IsSucess);

public class UpdateEstacionamentoCommandValidator : AbstractValidator<UpdateEstacionamentoCommand>
{
    public UpdateEstacionamentoCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("O Identificador único não pode ser nulo");
        RuleFor(x => x.DataEntrada).NotEmpty().WithMessage("Data de entrada não pode ser nulo");
        RuleFor(x => x.DataSaida).NotEmpty().WithMessage("Data de saída não pode ser nulo");
        RuleFor(x => x.VeiculoId).NotNull().WithMessage("O veículo não pode ser nulo");
    }
}

public class UpdateEstacionamentoCommandHandler 
    (ParkingContext dbContext, ParkingProtoService.ParkingProtoServiceClient parkingClient)
    : ICommandHandler<UpdateEstacionamentoCommand, UpdateEstacionamentoResult>
{
    public async Task<UpdateEstacionamentoResult> Handle(UpdateEstacionamentoCommand command, CancellationToken cancellationToken)
    {
        var estacionamento = await dbContext.Estacionamentos
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (estacionamento is null)
            throw new EntidadeNaoEncontradaException(command.Id);

        estacionamento.DataEntrada = command.DataEntrada;
        estacionamento.DataSaida = command.DataSaida;
        estacionamento.Duracao = command.DataSaida.HasValue 
            ? command.DataSaida.Value - command.DataEntrada 
            : null;
        estacionamento.VeiculoId = command.VeiculoId;

        var placaVeiculo = (await dbContext.Veiculos
            .FirstOrDefaultAsync(x => x.Id == command.VeiculoId))!.Placa;

        if (placaVeiculo is null)
            throw new BadRequestException("O veículo referenciado não possui placa, conclua o cadastro para finalizar!");

        if (estacionamento.DataSaida.HasValue)
        {
            var precoResponse = await parkingClient.CalcularValorFinalAsync(new CalcularValorRequest
            {
                Placa = placaVeiculo,
                DataEntrada = estacionamento.DataEntrada.ToString("O"),
                DataSaida = estacionamento.DataSaida.Value.ToString("O")
            }, cancellationToken: cancellationToken);

            estacionamento.ValorFinal = (decimal)precoResponse.ValorFinal;
        }

        dbContext.Estacionamentos.Update(estacionamento);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateEstacionamentoResult(true);
    }
}

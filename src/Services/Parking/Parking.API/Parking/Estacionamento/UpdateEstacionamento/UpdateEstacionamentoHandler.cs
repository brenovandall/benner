
namespace Parking.API.Parking.Estacionamento.UpdateEstacionamento;

public record UpdateEstacionamentoCommand(Guid Id, DateTime DataEntrada, DateTime? DataSaida, decimal? ValorFinal, TimeSpan? Duracao, Guid VeiculoId)
    : ICommand<UpdateEstacionamentoResult>;
public record UpdateEstacionamentoResult(bool IsSucess);

/// <summary>
/// Validador para os parametros informados em <see cref="UpdateEstacionamentoCommand"/>
/// </summary>
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
    /// <summary>
    /// Método para editar um estacionamento
    /// </summary>
    public async Task<UpdateEstacionamentoResult> Handle(UpdateEstacionamentoCommand command, CancellationToken cancellationToken)
    {
        var estacionamento = await dbContext.Estacionamentos
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        // Se não encontrar o estacionamento, retorna uma exceção personalizada
        if (estacionamento is null)
            throw new EntidadeNaoEncontradaException(command.Id);

        // Edita os parametros conforme enviado pela requisição
        estacionamento.DataEntrada = command.DataEntrada;
        estacionamento.DataSaida = command.DataSaida;
        estacionamento.Duracao = command.DataSaida.HasValue // A duração se trata da data de saída - data de entrada
            ? command.DataSaida.Value - command.DataEntrada 
            : null;
        estacionamento.VeiculoId = command.VeiculoId;

        var placaVeiculo = (await dbContext.Veiculos
            .FirstOrDefaultAsync(x => x.Id == command.VeiculoId))!.Placa;

        // Se não encontrar a placa do veiculo, retorna uma exceção personalizada
        if (placaVeiculo is null)
            throw new BadRequestException("O veículo referenciado não possui placa, conclua o cadastro para finalizar!");

        if (estacionamento.DataSaida.HasValue)
        {
            // Envia o request para o serviço gRPC para calcular o valor final
            var precoResponse = await parkingClient.CalcularValorFinalAsync(new CalcularValorRequest
            {
                DataEntrada = estacionamento.DataEntrada.ToString("O"),
                DataSaida = estacionamento.DataSaida.Value.ToString("O")
            }, cancellationToken: cancellationToken);

            estacionamento.ValorFinal = (decimal)precoResponse.ValorFinal; // Obtem o valor e aplica no campo do db
        }

        dbContext.Estacionamentos.Update(estacionamento); // Atualiza no db
        await dbContext.SaveChangesAsync(cancellationToken); // Faz o commit das alterações

        return new UpdateEstacionamentoResult(true);
    }
}

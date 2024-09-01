
namespace Parking.API.Parking.Veiculo.CreateVeiculo;

public record CreateVeiculoCommand(string Placa, string Modelo, string Cor)
    : ICommand<CreateVeiculoResult>;
public record CreateVeiculoResult(Guid Id);

/// <summary>
/// Validador para os parametros informados em <see cref="CreateVeiculoCommand"/>
/// </summary>
public class CreateVeiculoCommandValidator : AbstractValidator<CreateVeiculoCommand>
{
    public CreateVeiculoCommandValidator()
    {
        RuleFor(x => x.Placa).NotEmpty().WithMessage("A placa não pode ser nulo");
        RuleFor(x => x.Modelo).NotNull().WithMessage("O modelo não pode ser nulo");
        RuleFor(x => x.Cor).NotNull().WithMessage("A cor não pode ser nulo");
    }
}

public class CreateVeiculoCommandHandler (ParkingContext dbContext) 
    : ICommandHandler<CreateVeiculoCommand, CreateVeiculoResult>
{
    /// <summary>
    /// Método para criar um novo veículo
    /// </summary>
    public async Task<CreateVeiculoResult> Handle(CreateVeiculoCommand command, CancellationToken cancellationToken)
    {
        var veiculo = new Models.Veiculo
        {
            Placa = command.Placa,
            Modelo = command.Modelo,
            Cor = command.Cor,
        };

        await dbContext.Veiculos.AddAsync(veiculo); // Adiciona o veículo no db
        await dbContext.SaveChangesAsync(); // Faz o commit no db

        // Retorna o ID do novo registro
        return new CreateVeiculoResult(veiculo.Id);
    }
}

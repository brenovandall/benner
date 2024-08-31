
namespace Parking.API.Parking.Veiculo.CreateVeiculo;

public record CreateVeiculoCommand(string Placa, string Modelo, string Cor)
    : ICommand<CreateVeiculoResult>;
public record CreateVeiculoResult(Guid Id);

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
    public async Task<CreateVeiculoResult> Handle(CreateVeiculoCommand command, CancellationToken cancellationToken)
    {
        var veiculo = new Models.Veiculo
        {
            Placa = command.Placa,
            Modelo = command.Modelo,
            Cor = command.Cor,
        };

        await dbContext.Veiculos.AddAsync(veiculo);
        await dbContext.SaveChangesAsync();

        return new CreateVeiculoResult(veiculo.Id);
    }
}

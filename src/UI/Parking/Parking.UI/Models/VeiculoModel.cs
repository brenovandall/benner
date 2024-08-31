using System.ComponentModel.DataAnnotations;

namespace Parking.UI.Models;

public class Veiculo
{
    public Guid Id { get; set; }
    public string Placa { get; set; } = default!;
    public string Modelo { get; set; } = default!;
    public string Cor { get; set; } = default!;
}

public class VeiculoModel
{
    [Required(ErrorMessage = "A placa não pode ser nula")]
    public string Placa { get; set; } = default!;
    [Required(ErrorMessage = "O modelo não pode ser nulo")]
    public string Modelo { get; set; } = default!;
    [Required(ErrorMessage = "A cor não pode ser nula")]
    public string Cor { get; set; } = default!;
}

public class GetVeiculosResponse
{
    public IReadOnlyList<Veiculo> Veiculos { get; set; }
}

public class GetVeiculoByIdResponse
{
    public Veiculo Veiculo { get; set; }
}

public class CreateVeiculoResponse
{
    public Guid Id { get; set; }
}

public class UpdateVeiculoResponse
{
    public bool IsSuccess { get; set; }
}

public record CreateVeiculoRequest(string Placa, string Modelo, string Cor);
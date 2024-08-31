namespace Parking.API.Models;

public class Veiculo : BaseEntity
{
    public string Placa { get; set; } = default!;
    public string Modelo { get; set; } = default!;
    public string Cor { get; set; } = default!;
}

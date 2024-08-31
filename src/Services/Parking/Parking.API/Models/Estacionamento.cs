namespace Parking.API.Models;

public class Estacionamento : BaseEntity
{
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal? ValorFinal { get; set; }
    public TimeSpan? Duracao { get; set; }
    public Guid VeiculoId { get; set; }
}

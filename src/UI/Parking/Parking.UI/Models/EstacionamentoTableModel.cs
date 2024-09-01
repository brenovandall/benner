using System.Globalization;

namespace Parking.UI.Models;

public class EstacionamentoTableModel
{
    public Guid Id { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public decimal? ValorFinal { get; set; }
    public string ValorFinalReais => ValorFinal?.ToString("C", new CultureInfo("pt-BR"))!;
    public TimeSpan? Duracao { get; set; }
    public Veiculo? Veiculo { get; set; }
}

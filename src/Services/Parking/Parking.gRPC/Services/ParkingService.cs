using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Parking.gRPC.Data;
using Parking.gRPC.Models;

namespace Parking.gRPC.Services;

public class ParkingService (ParkingContext dbContext)
    : ParkingProtoService.ParkingProtoServiceBase
{
    public override async Task<CalcularValorResponse> CalcularValorFinal(CalcularValorRequest request, ServerCallContext context)
    {
        var dataEntrada = DateTime.Parse(request.DataEntrada);
        var dataSaida = DateTime.Parse(request.DataSaida);
        var duracao = dataSaida - dataEntrada;

        var precoBase = await dbContext.PrecosBase
            .Where(x => x.DataInicio <= dataEntrada && x.DataFim >= dataEntrada)
            .OrderByDescending(x => x.DataInicio)
            .FirstOrDefaultAsync();

        if (precoBase is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Tabela de preços não encontrada para a data de entrada informada."));

        double valorFinal = CalcularValor(duracao, precoBase);
        return new CalcularValorResponse { ValorFinal = valorFinal };
    }

    private double CalcularValor(TimeSpan duracao, PrecoBase precoBase)
    {
        if (duracao.TotalMinutes <= 30)
        {
            return (double)(precoBase.ValorHoraInicial / 2);
        }

        double valorFinal = (double)precoBase.ValorHoraInicial;

        var minutosAdicionais = duracao.TotalMinutes - 60;

        if (minutosAdicionais > 0)
        {
            var horasAdicionais = Math.Ceiling((minutosAdicionais - 10) / 60);

            if (horasAdicionais < 0) horasAdicionais = 0;


            valorFinal += (double)(horasAdicionais * (double)precoBase.ValorHoraAdicional);
        }

        return valorFinal;
    }
}

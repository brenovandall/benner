using Parking.UI.Models;
using Refit;

namespace Parking.UI.Services;

public interface IEstacionamentoService
{
    [Get("/estacionamento")]
    Task<GetEstacionamentosResponse> GetEstacionamentos();

    [Get("/estacionamento/{id}")]
    Task<GetEstacionamentoByIdResponse> GetEstacionamentoById(Guid id);

    [Post("/estacionamento")]
    Task<CreateEstacionamentoResponse> CreateEstacionamento(CreateEstacionamentoRequest request);

    [Put("/estacionamento")]
    Task<UpdateEstacionamentoResponse> UpdateEstacionamento(UpdateEstacionamentoRequest request);
}

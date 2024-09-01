namespace Parking.API.Exceptions;

// Exceção personalizada para quando alguma entidade não for encontrada
public class EntidadeNaoEncontradaException : NotFoundException
{
    public EntidadeNaoEncontradaException(Guid Id) : base("Entidade", Id) { }
}

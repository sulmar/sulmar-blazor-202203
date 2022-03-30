namespace Shopper.Domain.Repositories;

public interface IColorRepository
{
    Task<IEnumerable<string>> Get();
}


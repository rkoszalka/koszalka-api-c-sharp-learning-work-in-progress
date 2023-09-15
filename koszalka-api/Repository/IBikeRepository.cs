using koszalka_api.Model;

namespace koszalka_api.Repository;

public interface IBikeRepository
{
    Task<IEnumerable<Bike>> GetAllAsync();
    Task<Bike> GetByIdAsync(Int64 id);
    Task Create(Bike bike);
    Task Update(Bike bike);
    Task Delete(Int64 id);
}
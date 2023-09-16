using koszalka_api.DTO;
using koszalka_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Repository;

public interface IBikeRepository
{
    Task<IEnumerable<BikeDTO>> GetAllAsync();
    Task<BikeDTO> GetByIdAsync(Int64 id);
    Task<int> Create(BikeDTO bikeDto);
    Task<int> Update(BikeDTO bike);
    Task Delete(Int64 id);
}
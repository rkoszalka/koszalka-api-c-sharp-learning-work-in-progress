using koszalka_api.DTO;
using koszalka_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Service;

public interface IBikeService
{
    Task<IEnumerable<BikeDTO>> GetAllAsync();
    Task<BikeDTO> GetByIdAsync(long id);
    Task<int> Create(BikeDTO bikeDto);
    Task<int> Update(BikeDTO bike);
    Task Delete(long id);
}
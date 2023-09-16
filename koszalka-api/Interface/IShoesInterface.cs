using koszalka_api.DTO;
using koszalka_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Interface
{
    public interface IShoesInterface
    {
        Task<IEnumerable<ShoesDTO>> GetShoes();
    }
}

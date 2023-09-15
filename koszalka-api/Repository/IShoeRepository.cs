using koszalka_api.Model;

namespace koszalka_api.Repository
{
    public interface IShoeRepository
    {
        Task<IEnumerable<Shoes>> GetAllAsync();
    }
}

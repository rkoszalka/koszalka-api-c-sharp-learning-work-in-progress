using koszalka_api.Persistence.Model;

namespace koszalka_api.Persistence.Repository
{
    public interface IAuthRepository
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}

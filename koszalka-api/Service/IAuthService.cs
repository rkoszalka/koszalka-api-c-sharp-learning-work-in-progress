using koszalka_api.Persistence.Model;

namespace koszalka_api.Service
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}

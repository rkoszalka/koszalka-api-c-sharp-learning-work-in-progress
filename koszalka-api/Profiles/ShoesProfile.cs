using AutoMapper;
using koszalka_api.DTO;
using koszalka_api.Model;

namespace koszalka_api.Profiles
{
    public class ShoesProfile : Profile
    {
        public ShoesProfile()
        {
            CreateMap<Shoes, ShoesDTO>();
        }
    }
}

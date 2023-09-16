using AutoMapper;
using koszalka_api.DTO;
using koszalka_api.Model;

namespace koszalka_api.Profiles
{
    public class ShoesProfile : Profile
    {
        public ShoesProfile()
        {
            CreateMap<Shoes, ShoesDTO>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(x => x.Brand))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(x => x.Size));
        }
    }
}

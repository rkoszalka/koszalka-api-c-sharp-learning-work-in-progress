using AutoMapper;
using koszalka_api.Data;
using koszalka_api.DTO;
using koszalka_api.Interface;
using koszalka_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;


namespace koszalka_api.Service
{
    public class ShoesService : IShoesInterface
    {

        private readonly IMapper _mapper;
        private readonly EntityFrameworkConfigurationContext _dbContext;

        public ShoesService(IMapper mapper, EntityFrameworkConfigurationContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        

        public async Task<IEnumerable<ShoesDTO>> GetShoes()
        {
            List<Shoes> shoesResult = await _dbContext.Shoes.ToListAsync();

            if (shoesResult.Count.Equals(0))
            {
                return null;
            }

            var shoesDtos = _mapper.Map<IEnumerable<ShoesDTO>>(shoesResult);
            return shoesDtos;
        }
    }
}

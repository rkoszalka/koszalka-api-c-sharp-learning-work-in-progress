using AutoMapper;
using koszalka_api.Data;
using koszalka_api.DTO;
using koszalka_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.CodeAnalysis;

namespace koszalka_api.Service
{
    public class ShoesService : IShoesService
    {
        // @todo implement mapper, fix dependency injection problem
        private readonly IMapper _mapper;
        private readonly EntityFrameworkConfigurationContext _dbContext;

        public ShoesService(IMapper mapper, EntityFrameworkConfigurationContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        

        public async Task<ActionResult<IEnumerable<ShoesDTO>>> GetShoes()
        {
            List<Shoes> shoesResult = await _dbContext.Shoes.ToListAsync();

            if (shoesResult.Count.Equals(0))
            {
                return null;
            }

            return _mapper.Map<List<ShoesDTO>>(shoesResult);

        }


        public async Task<ShoesDTO> GetShoe(long id)
        {
            var response = await _dbContext.Shoes.FindAsync(id);
            return _mapper.Map<ShoesDTO>(response);
        }

        public async Task<Boolean> PutShoe(long id, Shoes shoes)
        {
            _dbContext.Entry(shoes).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoeExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<int> PostShoe(Shoes shoes)
        {
            _dbContext.Shoes.Add(shoes);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteShoe(long id)
        {
            var shoe = await _dbContext.Shoes.FindAsync(id);
            if (shoe == null)
            {
                return 0;
            }
            _dbContext.Shoes.Remove(shoe);
            return await _dbContext.SaveChangesAsync();
        }

        private bool ShoeExists(long id)
        {
            return (_dbContext.Shoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}

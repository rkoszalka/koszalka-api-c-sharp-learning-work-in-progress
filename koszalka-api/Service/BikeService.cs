using Dapper;
using System.Data;
using Microsoft.AspNetCore.OutputCaching;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using koszalka_api.Persistence.Model;
using koszalka_api.Persistence.Data;
using koszalka_api.Persistence.DTO;

namespace koszalka_api.Service

{
    public class BikeService : IBikeService
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;
        public BikeService(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BikeDTO>> GetAllAsync()
        {
            var query = "SELECT * FROM " + typeof(Bike).Name;
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Bike>(query);
                var bikeDto = _mapper.Map<IEnumerable<BikeDTO>>(result);
                return bikeDto;
            }
        }
        public async Task<BikeDTO> GetByIdAsync(long id)
        {

            var query = "SELECT * FROM " + typeof(Bike).Name + " WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Bike>(query, new { id });
                var resultDto = _mapper.Map<BikeDTO>(result);
                return resultDto;
            }
        }
        public async Task<int> Create(BikeDTO bikeDto)
        {
            var bikeEntity = _mapper.Map<Bike>(bikeDto);

            var query = "INSERT INTO  " + typeof(Bike).Name + " (Name, Description, Price, Model, Brand, CreatedDate,UpdatedDate) VALUES (@Name, @Description, @Price, @Model, @Brand, @CreatedDate, @UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", bikeEntity.Name, DbType.String);
            parameters.Add("Description", bikeEntity.Description, DbType.String);
            parameters.Add("Price", bikeEntity.Price, DbType.String);
            parameters.Add("Model", bikeEntity.Model, DbType.String);
            parameters.Add("Brand", bikeEntity.Brand, DbType.String);
            parameters.Add("CreatedDate", bikeEntity.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", bikeEntity.UpdatedDate, DbType.DateTime);
            using (var connection = _context.CreateConnection())
            {
                int rows = await connection.ExecuteAsync(query, parameters);
                return rows;
            }
        }

        public async Task<int> Update(BikeDTO bikeDto)
        {
            var bikeEntity = _mapper.Map<Bike>(bikeDto);

            var query = "UPDATE " + typeof(Bike).Name + " SET Name = @Name, Price = @Price, Model = @Model, Brand = @Brand, Description = @Description, UpdatedDate = @UpdatedDate WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Name", bikeEntity.Name, DbType.String);
            parameters.Add("Description", bikeEntity.Description, DbType.String);
            parameters.Add("Price", bikeEntity.Price, DbType.String);
            parameters.Add("Model", bikeEntity.Model, DbType.String);
            parameters.Add("Id", bikeEntity.Id, DbType.Int64);
            parameters.Add("Brand", bikeEntity.Brand, DbType.String);
            parameters.Add("CreatedDate", bikeEntity.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);
            using (var connection = _context.CreateConnection())
            {
                int rows = await connection.ExecuteAsync(query, parameters);
                return rows;
            }
        }
        public async Task Delete(long id)
        {
            var query = "DELETE FROM " + typeof(Bike).Name + " WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }

}

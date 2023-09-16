using Dapper;
using koszalka_api.Model;
using koszalka_api.Repository;
using System.Data;
using Microsoft.AspNetCore.OutputCaching;
using koszalka_api.Data;
using AutoMapper;
using koszalka_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Service

{
    public class BikeService : IBikeRepository
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
        public async Task<int> Create(Bike _Bike)
        {
            var query = "INSERT INTO  " + typeof(Bike).Name + " (Name, Description, Price, Model, Brand, CreatedDate,UpdatedDate) VALUES (@Name, @Description, @Price, @Model, @Brand, @CreatedDate, @UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", _Bike.Name, DbType.String);
            parameters.Add("Description", _Bike.Description, DbType.String);
            parameters.Add("Price", _Bike.Price, DbType.String);
            parameters.Add("Model", _Bike.Model, DbType.String);
            parameters.Add("Brand", _Bike.Brand, DbType.String);
            parameters.Add("CreatedDate", _Bike.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", _Bike.UpdatedDate, DbType.DateTime);
            using (var connection = _context.CreateConnection())
            {
                int rows = await connection.ExecuteAsync(query, parameters);
                return rows;
            }
        }

        public async Task<int> Update(Bike _Bike)
        {
            var query = "UPDATE " + typeof(Bike).Name + " SET Name = @Name, Price = @Price, Model = @Model, Brand = @Brand, Description = @Description, UpdatedDate = @UpdatedDate WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Name", _Bike.Name, DbType.String);
            parameters.Add("Description", _Bike.Description, DbType.String);
            parameters.Add("Price", _Bike.Price, DbType.String);
            parameters.Add("Model", _Bike.Model, DbType.String);
            parameters.Add("Id", _Bike.Id, DbType.Int64);
            parameters.Add("Brand", _Bike.Brand, DbType.String);
            parameters.Add("CreatedDate", _Bike.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", _Bike.UpdatedDate, DbType.DateTime);
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

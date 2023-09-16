using AutoMapper;
using koszalka_api.Data;
using koszalka_api.Implementation;
using koszalka_api.Profiles;
using koszalka_api.Repository;
using koszalka_api.Service;
using koszalka_api.Interface;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDapperContext, DapperContext>();
builder.Services.AddTransient<IBikeRepository, BikeRepository>();
builder.Services.AddTransient<EntityFrameworkConfigurationContext>();
builder.Services.AddTransient<ShoesService>();

builder.Services.AddAutoMapper(typeof(ShoesProfile));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

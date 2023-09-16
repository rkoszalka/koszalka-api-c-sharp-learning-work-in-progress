using AutoMapper;
using koszalka_api.Data;
using koszalka_api.Profiles;
using koszalka_api.Repository;
using koszalka_api.Service;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IBikeRepository, BikeService>();
builder.Services.AddTransient<IShoeRepository, ShoesService>();
builder.Services.AddTransient<EntityFrameworkConfigurationContext>();
builder.Services.AddTransient<DapperContext>();


builder.Services.AddAutoMapper(typeof(ShoesProfile));
builder.Services.AddAutoMapper(typeof(BikeProfile));

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

using System.Text.Json.Serialization;

namespace koszalka_api.Model
{
    public class BikeDTO
    {
        public Int64 Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
    }
}
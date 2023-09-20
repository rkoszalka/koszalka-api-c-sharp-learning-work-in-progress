namespace koszalka_api.Persistence.Model
{
    public class Bike : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
    }
}
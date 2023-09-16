namespace koszalka_api.Model
{
    public class BikeDTO : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
    }
}
using System.ComponentModel;

namespace koszalka_api.DTO
{
    public class ShoesDTO
    {
        public Int64 Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Brand { get; set; }
        public string? Size { get; set; }


    }
}

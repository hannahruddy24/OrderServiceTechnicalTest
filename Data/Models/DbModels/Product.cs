using System.ComponentModel.DataAnnotations;

namespace Data.Models.DbModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}

using Data.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.Context
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
            {
            }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

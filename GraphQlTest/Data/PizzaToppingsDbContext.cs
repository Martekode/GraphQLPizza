using GraphQlTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlTest.Data
{
    public class PizzaToppingsDbContext : DbContext
    {
        public PizzaToppingsDbContext(DbContextOptions<PizzaToppingsDbContext> options) : base(options)
        {
            
        }
        public DbSet<PizzaType> Pizzas { get; set; }
        public DbSet<ToppingType> Toppings { get; set; }
    }
}

using Bogus;
using GraphQlTest.Data;
using GraphQlTest.Models;
using GraphQlTest.Schema.Mutations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace GraphQlTest.Schema.Querries
{

    public class Query
    {
        private PizzaToppingsDbContext _context;
        public Query(IDbContextFactory<PizzaToppingsDbContext> context)
        {
            _context = context.CreateDbContext();
        }
        public ICollection<PizzaType> GetPizzas()
        {
            return _context.Pizzas.OrderBy(p => p.Id).ToList();
        }

        public ICollection<ToppingType> GetToppings()
        {
            return _context.Toppings.OrderBy(t => t.Id).ToList();
        }
    }
}

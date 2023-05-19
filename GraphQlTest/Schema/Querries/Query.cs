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
        private IDbContextFactory<PizzaToppingsDbContext> _context;
        public Query(IDbContextFactory<PizzaToppingsDbContext> context)
        {
            _context = context;
        }
        public ICollection<PizzaType> GetPizzas()
        {
            using (PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                return context.Pizzas.OrderBy(p => p.Id).ToList();
            }
        }

        public ICollection<ToppingType> GetToppings()
        {
            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                return context.Toppings.OrderBy(t => t.Name).ToList();
            }
        }
    }
}

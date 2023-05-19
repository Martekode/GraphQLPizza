using Bogus;
using GraphQlTest.Data;
using GraphQlTest.Exeptions;
using GraphQlTest.Models;
using GraphQlTest.Schema.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphQlTest.Schema.Mutations
{
    
    public class Mutation 
    {
        private IDbContextFactory<PizzaToppingsDbContext> _context;
        public Mutation(IDbContextFactory<PizzaToppingsDbContext> pizzaToppingDbContext)
        {
            _context = pizzaToppingDbContext;
        }


        /// <summary>
        /// resolver to CREATE a topping.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<ToppingType> CreateTopping(ToppingInputType toppingInputType, [Service] ITopicEventSender topicEventSender)
        {
            if (_context.CreateDbContext().Toppings.Any(t => t.Name == toppingInputType.Name))
                throw new GraphQLException("TOPPING_ALREADY_EXISTS");

            ToppingType topping = new ToppingType()
            {
                Id = Guid.NewGuid(),
                Name = toppingInputType.Name,
                Price = toppingInputType.Price
            };

            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                context.Toppings.Add(topping);
                await context.SaveChangesAsync();
            }
            await topicEventSender.SendAsync(nameof(Subscription.ToppingCreated), topping);

            return topping;
        }

        /// <summary>
        /// resolver to UPDATE an already existing topping.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="toppingId"></param>
        /// <returns></returns>
        /// <exception cref="GraphQLException"></exception>
        public ToppingType UpdateTopping(ToppingInputType toppingInputType , Guid toppingId) { 

            ToppingType topping = _context.CreateDbContext().Toppings.FirstOrDefault(t => t.Id ==  toppingId);

            if (topping == null)
                throw new NotFoundExeption("Not Found");

            topping.Name = toppingInputType.Name;
            topping.Price = toppingInputType.Price;

            using (PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                context.Toppings.Update(topping);
                context.SaveChanges();
            }
                return topping;
        }

        /// <summary>
        /// resolver to DELETE a topping via the GUID.
        /// </summary>
        /// <param name="toppingId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTopping(Guid toppingId)
        {
            bool toppingExists = _context.CreateDbContext().Toppings.Any(t => t.Id == toppingId);

            if(!toppingExists)
                throw new NotFoundExeption("Not Found");

            ToppingType topping = new ToppingType()
            {
                Id = toppingId
            };

            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                context.Toppings.Remove(topping);
                return await context.SaveChangesAsync() > 0;
            }
        }


        /// <summary>
        /// resolever to CREATE a pizza. 
        /// </summary>
        /// <param name="pizzaBase"></param>
        /// <param name="crust"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<PizzaType> CreatePizza(PizzaInputType pizzaInputType, [Service] ITopicEventSender topicEventSender)
        {
            ToppingType topping;
            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                topping = await context.Toppings.FirstOrDefaultAsync(t => t.Id == pizzaInputType.ToppingId);
            }

            if (topping == null)
                throw new NotFoundExeption("Not Found");

            PizzaType pizza = new PizzaType()
            {
                Id = Guid.NewGuid(),
                Base = pizzaInputType.pizzaBase,
                Crust = pizzaInputType.crust,
                Size = pizzaInputType.size,
                ToppingId = topping.Id
            };

            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                context.Pizzas.Add(pizza);
                context.SaveChanges();
            }
            await topicEventSender.SendAsync(nameof(Subscription.PizzaCreated), pizza);

            return pizza;
        }

        /// <summary>
        /// resolver to UPDATE already existing pizza. 
        /// </summary>
        /// <param name="pizzaId"></param>
        /// <param name="newBase"></param>
        /// <param name="newCrust"></param>
        /// <param name="newSize"></param>
        /// <param name="needNewTopping"></param>
        /// <returns></returns>
        /// <exception cref="GraphQLException"></exception>
        public async Task<PizzaType> UpdatePizza(Guid pizzaId, UpdatePizzaInputType pizzaInputType , Guid newToppingId)
        {
            PizzaType pizza;
            ToppingType newTopping;
            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                pizza = await context.Pizzas.FirstOrDefaultAsync(p => p.Id == pizzaId);
                newTopping = await context.Toppings.FirstOrDefaultAsync(t => t.Id == newToppingId);
            }

            if (pizza == null)
                throw new NotFoundExeption("Pizza Not Found");
            

            if (newTopping == null)
                throw new NotFoundExeption("Topping Not Found");


            pizza.Base = pizzaInputType.pizzaBase;
            pizza.Crust = pizzaInputType.crust;
            pizza.Size = pizzaInputType.size;
            pizza.ToppingId = newToppingId;

            using(PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                context.Pizzas.Update(pizza);
                context.SaveChanges();
            }

            return pizza;
        }

        /// <summary>
        /// resolver to DELETE a pizza based on GUID.
        /// </summary>
        /// <param name="pizzaId"></param>
        /// <returns></returns>
        public async Task<bool> DeletePizza(Guid pizzaId )
        {
            bool pizzaExists = _context.CreateDbContext().Pizzas.Any(t => t.Id == pizzaId);

            if (!pizzaExists)
                throw new NotFoundExeption("Not Found");

            PizzaType pizza = new PizzaType()
            {
                Id = pizzaId
            };

            using (PizzaToppingsDbContext context = _context.CreateDbContext())
            {
                context.Pizzas.Remove(pizza);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}

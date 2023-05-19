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
        private PizzaToppingsDbContext _context;
        public Mutation(IDbContextFactory<PizzaToppingsDbContext> pizzaToppingDbContext)
        {
            _context = pizzaToppingDbContext.CreateDbContext();
        }


        /// <summary>
        /// resolver to CREATE a topping.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<ToppingType> CreateTopping(ToppingInputType toppingInputType, [Service] ITopicEventSender topicEventSender)
        {
            if (await _context.Toppings.AnyAsync(t => t.Name == toppingInputType.Name))
                throw new GraphQLException("TOPPING_ALREADY_EXISTS");

            ToppingType topping = new ToppingType()
            {
                Id = Guid.NewGuid(),
                Name = toppingInputType.Name,
                Price = toppingInputType.Price
            };

           
            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();
            
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
        public async Task<ToppingType> UpdateTopping(ToppingInputType toppingInputType , Guid toppingId) { 

            ToppingType topping = await _context.Toppings.FirstOrDefaultAsync(t => t.Id ==  toppingId);

            if (topping == null)
                throw new NotFoundExeption("Not Found");

            topping.Name = toppingInputType.Name;
            topping.Price = toppingInputType.Price;

            
            _context.Toppings.Update(topping);
            await  _context.SaveChangesAsync();
            
                return topping;
        }

        /// <summary>
        /// resolver to DELETE a topping via the GUID.
        /// </summary>
        /// <param name="toppingId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTopping(Guid toppingId)
        {
            bool toppingExists = await _context.Toppings.AnyAsync(t => t.Id == toppingId);

            if(!toppingExists)
                throw new NotFoundExeption("Not Found");

            ToppingType topping = new ToppingType()
            {
                Id = toppingId
            };

            
            _context.Toppings.Remove(topping);
            return await _context.SaveChangesAsync() > 0;
           
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
            ToppingType topping = await _context.Toppings.FirstOrDefaultAsync(t => t.Id == pizzaInputType.ToppingId);

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

           
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
            
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
            PizzaType pizza = await _context.Pizzas.FirstOrDefaultAsync(p => p.Id == pizzaId);

            if (pizza == null)
                throw new NotFoundExeption("Pizza Not Found");

            ToppingType newTopping = await _context.Toppings.FirstOrDefaultAsync(t => t.Id == newToppingId);   

            if (newTopping == null)
                throw new NotFoundExeption("Topping Not Found");


            pizza.Base = pizzaInputType.pizzaBase;
            pizza.Crust = pizzaInputType.crust;
            pizza.Size = pizzaInputType.size;
            pizza.ToppingId = newToppingId;

            
            _context.Pizzas.Update(pizza);
            await _context.SaveChangesAsync();
            

            return pizza;
        }

        /// <summary>
        /// resolver to DELETE a pizza based on GUID.
        /// </summary>
        /// <param name="pizzaId"></param>
        /// <returns></returns>
        public async Task<bool> DeletePizza(Guid pizzaId )
        {
            bool pizzaExists = await _context.Pizzas.AnyAsync(t => t.Id == pizzaId);

            if (!pizzaExists)
                throw new NotFoundExeption("Not Found");

            PizzaType pizza = new PizzaType()
            {
                Id = pizzaId
            };

            _context.Pizzas.Remove(pizza);
            return await _context.SaveChangesAsync() > 0;
            
        }
    }
}

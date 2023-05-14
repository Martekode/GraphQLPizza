using Bogus;
using GraphQlTest.Models;
using GraphQlTest.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQlTest.Schema.Mutations
{
    
    public class Mutation
    {
        public List<ToppingResult> _toppings;
        public List<PizzaResult> _pizzas;
        public Faker<ToppingType> _toppingFaker;
        public Mutation()
        {
            _toppings = new List<ToppingResult>();
            _pizzas = new List<PizzaResult>();
            _toppingFaker = new Faker<ToppingType>()
                .RuleFor(t => t.Id , Guid.NewGuid())
                .RuleFor(t => t.Name , f => f.Commerce.ProductMaterial())
                .RuleFor(t => t.Price , f => f.Random.Double());
        }


        /// <summary>
        /// resolver to CREATE a topping.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<ToppingResult> CreateTopping(ToppingInputType toppingInputType, [Service] ITopicEventSender topicEventSender)
        {
            ToppingResult topping = new ToppingResult()
            {
                Id = Guid.NewGuid(),
                Name = toppingInputType.Name,
                Price = toppingInputType.Price
            };

            _toppings.Add(topping);
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
        public ToppingResult UpdateTopping(ToppingInputType toppingInputType , Guid toppingId) { 
            ToppingResult topping = _toppings.FirstOrDefault(t => t.Id == toppingId);

            if (topping == null)
            {
                throw new GraphQLException("This id does not match an existing topping!");
            }

            topping.Name = toppingInputType.Name;
            topping.Price = toppingInputType.Price;

            return topping;
        }

        /// <summary>
        /// resolver to DELETE a topping via the GUID.
        /// </summary>
        /// <param name="toppingId"></param>
        /// <returns></returns>
        public bool DeleteTopping(Guid toppingId)
        {
            return _toppings.RemoveAll(t => t.Id == toppingId) >= 1;
        }


        /// <summary>
        /// resolever to CREATE a pizza. 
        /// Topping is generated randomly for now sinds we dont have a database yet. 
        /// </summary>
        /// <param name="pizzaBase"></param>
        /// <param name="crust"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<PizzaResult> CreatePizza(PizzaInputType pizzaInputType, [Service] ITopicEventSender topicEventSender)
        {
            PizzaResult pizza = new PizzaResult()
            {
                Id = Guid.NewGuid(),
                Base = pizzaInputType.pizzaBase,
                Crust = pizzaInputType.crust,
                Size = pizzaInputType.size,
                Topping = _toppingFaker.Generate()
            };

            _pizzas.Add(pizza);
            await topicEventSender.SendAsync(nameof(Subscription.PizzaCreated), pizza);

            return pizza;
        }

        /// <summary>
        /// resolver to UPDATE already existing pizza. 
        /// It also takes a boolean to check if you want a new topping. 
        /// And generates it for you.
        /// </summary>
        /// <param name="pizzaId"></param>
        /// <param name="newBase"></param>
        /// <param name="newCrust"></param>
        /// <param name="newSize"></param>
        /// <param name="needNewTopping"></param>
        /// <returns></returns>
        /// <exception cref="GraphQLException"></exception>
        public PizzaResult UpdatePizza(Guid pizzaId, PizzaInputType pizzaInputType , bool needNewTopping)
        {
            PizzaResult pizza = _pizzas.FirstOrDefault(p => p.Id == pizzaId);

            if (pizza == null)
            {
                throw new GraphQLException("The pizza does not exist with this Id: " +  pizzaId);
            }

            if(needNewTopping)
            {
                pizza.Topping = _toppingFaker.Generate();
            }

            pizza.Base = pizzaInputType.pizzaBase;
            pizza.Crust = pizzaInputType.crust;
            pizza.Size = pizzaInputType.size;

            return pizza;
        }

        /// <summary>
        /// resolver to DELETE a pizza based on GUID.
        /// It will return bool wether it deleted it or not.
        /// </summary>
        /// <param name="pizzaId"></param>
        /// <returns></returns>
        public bool DeletePizza(Guid pizzaId )
        {
            return _pizzas.RemoveAll(p => p.Id == pizzaId) >= 1;
        }
    }
}

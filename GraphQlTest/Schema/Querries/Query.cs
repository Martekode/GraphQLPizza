using Bogus;
using GraphQlTest.Models;
using GraphQlTest.Schema.Mutations;
using Microsoft.VisualBasic;

namespace GraphQlTest.Schema.Querries
{
    public class Query
    {
        public ICollection<PizzaType> GetPizzas()
        {
            // list collection of the generated pizzas by bogus
            ICollection<PizzaType> pizzas = new List<PizzaType>
            {
                new PizzaType
                {
                    Id = Guid.NewGuid(),
                    Base = PizzaBase.GrainDhough,
                    Crust = CrustThickness.Thick,
                    Size = Sizes.S,
                    Topping = new ToppingType
                    {
                        Id= Guid.NewGuid(),
                        Name = "peperoni",
                        Price = 2.00
                    }
                },
                new PizzaType
                {
                    Id = Guid.NewGuid(),
                    Base = PizzaBase.FlourDhough,
                    Crust = CrustThickness.Thick,
                    Size = Sizes.L,
                    Topping = new ToppingType
                    {
                        Id= Guid.NewGuid(),
                        Name = "Hawaii",
                        Price = 4.00
                    }
                }

            };
            
            // return the pizzaList
            return pizzas;
        }

        public ICollection<ToppingResult> GetToppings()
        {
            List<ToppingResult> toppings = new List<ToppingResult>
            {
                new ToppingResult
                {
                    Id = Guid.NewGuid(),
                    Name = "test",
                    Price = 1
                },
                new ToppingResult
                {
                    Id = Guid.NewGuid(),
                    Name= "test2",
                    Price = 2
                }
            };

            return toppings;
        }
    }
}

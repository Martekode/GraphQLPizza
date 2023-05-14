using GraphQlTest.Models;

namespace GraphQlTest.Schema.Mutations
{
    public class PizzaResult
    {
        public Guid Id { get; set; }
        public PizzaBase Base { get; set; }
        public CrustThickness Crust { get; set; }
        public Sizes Size { get; set; }
        public ToppingType Topping { get; set; }
    }
}

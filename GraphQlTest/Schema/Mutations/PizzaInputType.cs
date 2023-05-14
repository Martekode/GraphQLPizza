using GraphQlTest.Models;

namespace GraphQlTest.Schema.Mutations
{
    public class PizzaInputType
    {
        public PizzaBase pizzaBase { get; set; }
        public CrustThickness crust { get; set; }
        public Sizes size { get; set; }
    }
}

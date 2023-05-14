using System.Security.Cryptography;

namespace GraphQlTest.Models
{
    public enum PizzaBase
    {
        FlourDhough,
        GrainDhough
    }

    public enum CrustThickness
    {
        Thick,
        Thin
    }

    public enum Sizes
    {
        L,
        M,
        S
    }
    public class PizzaType
    {
        public Guid Id { get; set; }
        public PizzaBase Base { get; set; }

        public CrustThickness Crust { get; set; }
        public Sizes Size { get; set; }

        public ToppingType Topping { get; set; }
    }
}

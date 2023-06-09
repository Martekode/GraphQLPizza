﻿using GraphQlTest.Models;

namespace GraphQlTest.Schema.Mutations
{
    public class PizzaInputType
    {
        public PizzaBase pizzaBase { get; set; }
        public CrustThickness crust { get; set; }
        public Sizes size { get; set; }
        public Guid ToppingId { get; set; }
    }
    public class UpdatePizzaInputType
    {
        public PizzaBase pizzaBase { get; set; }
        public CrustThickness crust { get; set; }
        public Sizes size { get; set; }
    }
}

using GraphQlTest.Models;
using GraphQlTest.Schema.Mutations;

namespace GraphQlTest.Schema.Subscriptions
{
    public class Subscription
    {
        /// <summary>
        /// Subscription that RETURNS the CREATED TOPPING when created.
        /// </summary>
        /// <param name="topping"></param>
        /// <returns></returns>
        [Subscribe]
        public ToppingResult ToppingCreated([EventMessage] ToppingResult topping) => topping;

        /// <summary>
        /// subscription that RETURNS the CREATED PIZZA when created.
        /// </summary>
        /// <param name="pizza"></param>
        /// <returns></returns>
        [Subscribe]
        public PizzaResult PizzaCreated([EventMessage] PizzaResult pizza) => pizza;
    }
}

using GraphQlTest.Models;

namespace GraphQlTest.Schema.Mutations
{
    public class ToppingResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

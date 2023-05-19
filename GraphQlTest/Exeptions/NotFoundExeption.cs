using HotChocolate.Execution;

namespace GraphQlTest.Exeptions
{
    public class NotFoundExeption : QueryException
    {
        public NotFoundExeption(string message) : base(message) 
        { 
        }
    }
}

namespace GraphQlTest.Exeptions
{
    public class NotFoundErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if(error is NotFoundExeption)
            {
                var message = "Not Found";
                var extensions = new Dictionary<string, object>
                {
                    {"code", "NOT_FOUND" }
                };

                return error.WithMessage(message).WithExtensions(extensions);
            }
            return error;
        }
    }
}

namespace DemoApplication.Exceptions
{
    public class IdentityCookieNotFound : Exception
    {
        public IdentityCookieNotFound(string? message)
            : base(message)
        {

        }
    }
}

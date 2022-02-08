namespace CustomExceptions
{
    public class InterlServerErrorException : LogException
    {
        public InterlServerErrorException() { }

        public InterlServerErrorException(string entity, string message) : base(entity, message)
        {
        }

        public InterlServerErrorException(string entity, string message, Exception inner)
        : base(entity, message, inner)
        {
        }
    }
}

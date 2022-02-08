namespace CustomExceptions
{
    public class BadRequestException : LogException
    {
        public BadRequestException() { }

        public BadRequestException(string entity, string message) : base(entity, message)
        {
        }

        public BadRequestException(string entity, string message, Exception inner)
        : base(entity, message, inner)
        {
        }
    }
}

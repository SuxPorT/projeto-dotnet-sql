namespace Exceptions
{
    public class BadRequestException : LogException
    {
        public BadRequestException() { }

        public BadRequestException(string entity, string message) : base(entity, message)
        {
            Entity = entity;
        }

        public BadRequestException(string entity, string message, Exception inner)
        : base(entity, message, inner)
        {
            Entity = entity;
        }
    }
}

namespace Exceptions
{
    public class NotFoundException : LogException
    {
        public NotFoundException() { }

        public NotFoundException(string entity, string message) : base(entity, message)
        {
            Entity = entity;
        }

        public NotFoundException(string entity, string message, Exception inner)
        : base(entity, message, inner)
        {
            Entity = entity;
        }
    }
}

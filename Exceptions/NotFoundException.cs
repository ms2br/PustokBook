namespace PustokBook.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("value not found")
        {
        }
    }
}

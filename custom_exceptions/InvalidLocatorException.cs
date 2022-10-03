namespace EcommerceDemo.custom_exceptions
{
    public class InvalidLocatorException : Exception
    {
        public InvalidLocatorException(string? message) : base(message)
        {
            Console.WriteLine("Locator = {0}", message);
            Console.WriteLine("This locator type is invalid. Locator definition must start by 'id=', 'css=', 'class=', 'name=' or 'xpath='.");
            Console.WriteLine("Ex: css=div#date-picker");
        }
    }
}

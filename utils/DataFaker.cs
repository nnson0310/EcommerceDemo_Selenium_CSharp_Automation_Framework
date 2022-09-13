namespace EcommerceDemo.utils
{
    public class DataFaker
    {
        public static string GetRandomFirstName()
        {
            return Faker.Name.First();
        }

        public static string GetRandomLastName()
        {
            return Faker.Name.Last();
        }

        public static string GetRandomFullName()
        {
            return Faker.Name.FullName();
        }

        public static string GetRandomEmail()
        {
            return Faker.Internet.Email();
        }
    }
}

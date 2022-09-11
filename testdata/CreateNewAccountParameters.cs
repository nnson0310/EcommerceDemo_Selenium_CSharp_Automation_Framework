
namespace EcommerceDemo.testdata
{
    public class CreateNewAccountParameters
    {
        public static object[] InvalidFirstName =
        {
            new object[] {"david@!$#", "copper", "david@gmail.com", "David123", "David123"}
        };

        public static object[] InvalidEmail =
        {
            new object[]{"david@"},
            new object[]{"david$gmail" },
            new object[]{"david@.com" }
        };

    }    
}

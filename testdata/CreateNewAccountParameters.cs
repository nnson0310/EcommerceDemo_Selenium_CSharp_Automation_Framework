
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

        public static object[] InvalidLengthPassword =
        {
            new object[] {"123456"}
        };

        public static object[] ThreeClassesCharPassword =
        {
            new object[]{"12345678"},
            new object[]{"david123"},
            new object[]{"DAVID123"},
            new object[]{"$#@!#@!!!!"},
            new object[]{"DAVIDCOPPER"},
            new object[]{"$$$123456"},
            new object[]{"DAVID####"},
            new object[]{"DavidCopper"},
            new object[]{"david####"}
    };

    }
}

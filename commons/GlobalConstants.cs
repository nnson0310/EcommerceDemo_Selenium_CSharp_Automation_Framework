namespace EcommerceDemo.commons
{
    public sealed class GlobalConstants
    {
        public const int longTimeout = 20;
        public const int shortTimeout = 10;
        public static IReadOnlyList<string> optionArguments = new List<string>()
        {
            "--disable-gpu",
            "--disable-popup-blocking",
            "--disable-notifications",
            "--start-maximized",
            "--no-sandbox",
            "--dns-prefetch-disable",
            "--ignore-certificate-errors",
            "disable-infobars",
            "--enable-automation"
        };
    }
}

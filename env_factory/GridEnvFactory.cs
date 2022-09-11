using OpenQA.Selenium;

namespace EcommerceDemo.env_factory
{
    public class GridEnvFactory: IEnvFactory
    {
        private string browserName;
        private string ipAddress;
        private string port;

        public GridEnvFactory(string browserName, string ipAddress, string port)
        {
            this.browserName = browserName;
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public IWebDriver InitDriver()
        {
            throw new NotImplementedException();
        }
    }
}

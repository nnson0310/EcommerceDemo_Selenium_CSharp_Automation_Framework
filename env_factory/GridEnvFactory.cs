using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemo.env_factory
{
    internal class GridEnvFactory: IEnvFactory
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

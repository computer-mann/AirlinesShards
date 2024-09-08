using AirlinesApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Airlines
{
    internal class AirlineApplicationFactory:WebApplicationFactory<Program>
    {

        public AirlineApplicationFactory()
        {
            
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                
            });
        }
    }
}

using AirlinesApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
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
                config.AddInMemoryCollection([
                    new KeyValuePair<string, string?>("ConnectionStrings:AirlineDb", "Host=localhost;Port=5432;Database=AirlineDb;Username=postgres;Password=postgres"),
                    new KeyValuePair<string, string?>("ConnectionStrings:Seq", "http://localhost:5341"),
                    new KeyValuePair<string, string?>("ConnectionStrings:Redis", "localhost:6379"),
                    new KeyValuePair<string, string?>("ConnectionStrings:Jaeger", "localhost:6831"),
                    new KeyValuePair<string, string?>("ConnectionStrings:Zipkin", "http://localhost:9411/api/v2/spans"),
                    ]);
                config.AddJsonFile("integration.json");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UnityArenaApi.Persistence.Context;
namespace UnityArenaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHost(args);
            using(var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<AppDbContext>()){
                    context.Database.EnsureCreated();
                }
            }
            host.Run();
        }
        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}

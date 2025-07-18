using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApplication2;

namespace WebApplication2   // ⬅️ usa tu namespace real
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Host + carga de appsettings, logging, etc.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Toda la lógica vive en Startup.cs
                    webBuilder.UseStartup<Startup>();
                });
    }
}
